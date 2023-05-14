using System.Collections;
using UnityEngine;

public class GlobalLogic : MonoBehaviour {
    private const float scoreDelay = 1.25f;
    private const float scoreIncrementTickRate = 0.02f;
    private const float maxVictoryConditionWait = 3f;

    private static GUIStyle menuTipStringGUI; // Used for the alternative text used for the text on the top-right

    public static GlobalLogic Instance;

    [SerializeField]
    private int requiredScore = 500; // The amount of score the player must end up with to win
    [SerializeField]
    internal int throws = 4; // Number of throws the player is allowed in the current level
    [SerializeField]
    private Rigidbody[] meltableObjects; // Cache the meltable objects so we don't need to search for them at runtime
    [SerializeField]
    private GUISkin guiSkin; // A custom GUISkin used for the menu
    [SerializeField]
    private Texture2D success; // Texture used if the player succeeded
    [SerializeField]
    private Texture2D failure; // Texture used if the play lost

    internal bool finished = false;
    internal bool inGameMenu = false;

    private int levelPoints = 0;
    internal float strategyBonus = 0f;

    private int score = 0;
    private int queuedScore = 0;
    private float scoreCountdown;

    private int scoreIncrement = 1; // The amount of score which is added per tick (scoreIncrementTickRate)
    private bool movement = true;

    private void Awake() {
        if(Instance != null) {
            Debug.LogWarning("There is more than one Global Logic script in this scene. There should only ever be one");
        }
        Instance = this;
    }

    private IEnumerator Start() {
        // Reset this variables because otherwise it will be kept through each level
        strategyBonus = 0f;
        inGameMenu = false;

        menuTipStringGUI = new GUIStyle();
        menuTipStringGUI.normal.textColor = new Color(0.7f, 0.7f, 0.7f);
        menuTipStringGUI.fontStyle = FontStyle.Italic;

        // Begin checking for when it is the right time to upload the players highscore
        yield return StartCoroutine(WaitForFinish());

        while(true) {
            yield return null;
            scoreCountdown -= Time.deltaTime;

            if(scoreCountdown <= 0f) {
                /**
			    * Increment the score based on it's size. We must use a positive number
			    * for this to work, which is what Mathf.Abs is used for. The "Mathf.Abs"
			    * function always returns the positive version of the number.
			    * The number is then multiplied by 0.06 so the queuedScore is carried over quicker
			    * the bigger the number.
			    * 
			    * The increment must always be 1 or greater, so the Mathf.Max function is used
			    */
                scoreIncrement = Mathf.RoundToInt(Mathf.Max(Mathf.Abs(queuedScore) * 0.06f, 1f));

                /**
			    * Negative Score (ie, the score is -20)
			    * This check is in place if you (the user of this source code) decide to implement score penalties. 
			    * Possible with: GlobalLogic.RemoveScore(int)
			    */
                if(queuedScore < 0) {
                    yield return new WaitForSeconds(scoreIncrementTickRate); //  Throttle the incrementation by the "incrementRate"

                    /**
                    * Add the score increment to queuedScore. This effectively continues to run until the 
                    * queuedScore is no longer negative.
                    */
                    queuedScore += scoreIncrement;
                    score -= scoreIncrement; // Take the score increment off score
                                             // Positive Score
                } else if(queuedScore > 0) {
                    yield return new WaitForSeconds(scoreIncrementTickRate);
                    queuedScore -= scoreIncrement;
                    score += scoreIncrement;
                }
            }
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            // Menu Toggle
            if(inGameMenu) {
                inGameMenu = false;
            } else {
                inGameMenu = true;
            }
        }
    }

    internal IEnumerator FinishedLevel() {
        /**
	    * Check if any objects are moving - it should not show the victory condition screen until
	    * the player has run out of throws and all objects are still (indicating no further scoring 
	    * can happen)
	    */

        // Wait a little before checking since the last projectile still needs to hit the target
        yield return new WaitForSeconds(3f);
        StartCoroutine(VictoryConditionTimeout()); // Start the timeout function to make sure we don't wait to long

        while(movement == true) {
            yield return new WaitForSeconds(0.2f); // Check 5 times a second

            // Check for if the wait for all objects to sleep has been overriden
            if(finished == true) {
                movement = false; // Pretend everything has stopped moving so the player can advance
                break;
            }

            yield return new WaitForFixedUpdate();
            movement = false; // Assume movement is false
            foreach(Rigidbody rb in meltableObjects) {
                if(rb == null) continue;
                if(rb.IsSleeping() == false) { // Check if the rigidbody is moving (not sleeping)
                    movement = true;
                    break;
                }
            }
        }

        // The delay between when the victory condition screen will show up after all objects are stationary
        yield return new WaitForSeconds(0.5f);
        finished = true;
    }

    private IEnumerator VictoryConditionTimeout() {
        /**
	    * In case some objects are moving very slowly, but aren't sleeping anytime soon, the finish condition
	    * should be overridden
	    */
        yield return new WaitForSeconds(maxVictoryConditionWait);
        finished = true;
    }

    internal void AddScore(int count) {
        scoreCountdown = scoreDelay;
        queuedScore += count;
    }

    internal void RemoveScore(int count) {
        scoreCountdown = scoreDelay;
        queuedScore -= count;
    }

    private IEnumerator WaitForFinish() {
        while(finished == false) {
            yield return null;
        }

        /**
	    * The strategy bonus is a deterent from people spamming the fire to cheat through levels
	    * Here anything below 1 is not allowed because it will take the score away. To prevent anyone from
	    * gaming the system, the cut off strategy multiplier is 10
	    */
        strategyBonus = Mathf.Clamp(strategyBonus, 1f, 10f);
        levelPoints = Mathf.RoundToInt(strategyBonus * (score + queuedScore));
    }

    private void OnGUI() {
        GUI.Box(new Rect(5, 5, 185, 70), GUIContent.none);
        GUI.Label(new Rect(15, 10, 170, 30), "Score:\t" + score.ToString() + " (+" + queuedScore + ") of " + requiredScore);
        GUI.Label(new Rect(15, 27, 170, 30), "Throws:\t" + throws);
        GUI.Label(new Rect(15, 44, 170, 30), "Projectile:\t" + MouseOrbit.Instance.projectileTypeLabel);

        // Menu Button
        GUI.Label(new Rect(Screen.width - 190, 5, 205, 25), "Press Escape to restart or quit", menuTipStringGUI);

        int currentSceneIndex = Application.loadedLevel;

        if(finished) {
            GUI.skin = guiSkin;
            GUI.Box(new Rect(Screen.width * 0.5f - 225f, Screen.height * 0.5f - 130f, 450f, 300f), "Game Finished");
            GUI.Label(new Rect(Screen.width * 0.5f - 100f, Screen.height * 0.5f - 100f, 200f, 30f),
                "Score: " + (score + queuedScore) + "/" + requiredScore);
            GUI.Label(new Rect(Screen.width * 0.5f - 100f, Screen.height * 0.5f - 80f, 200f, 30f),
                "Patience Multiplier: " + strategyBonus.ToString("0.00") + "x");
            levelPoints = Mathf.RoundToInt(strategyBonus * (score + queuedScore));
            GUI.Label(new Rect(Screen.width * 0.5f - 100f, Screen.height * 0.5f - 60f, 200f, 30f), "Points: " + levelPoints);

            // If the players score plus the queued score exceeds what is required, they won!
            if(score + queuedScore >= requiredScore) {
                GUI.DrawTexture(new Rect(Screen.width * 0.5f - 200f, Screen.height * 0.5f - 10f, 400f, 87.5f), success);
                // If the current level is the last level
                if(currentSceneIndex == Application.levelCount - 1) {
                    if(GUI.Button(new Rect(Screen.width * 0.5f + 95f, Screen.height * 0.5f + 110f, 120f, 30f), "Finished")) {
                        Application.LoadLevel(0); // The player finished the game! So return them to the main menu
                    }
                } else {
                    if(GUI.Button(new Rect(Screen.width * 0.5f + 95f, Screen.height * 0.5f + 110f, 120f, 30f), "Next Level")) {
                        Application.LoadLevel(currentSceneIndex + 1);
                    }
                }
                if(GUI.Button(new Rect(Screen.width * 0.5f - 60f, Screen.height * 0.5f + 110f, 120f, 30f), "Retry Level")) {
                    Application.LoadLevel(currentSceneIndex);
                }
                // If the player has lost (or at least, according to the current score, maybe some objects will fall to the ground)
            } else {
                GUI.DrawTexture(new Rect(Screen.width * 0.5f - 200f, Screen.height * 0.5f - 10f, 400f, 87.5f), failure);
                if(GUI.Button(new Rect(Screen.width * 0.5f + 95f, Screen.height * 0.5f + 110f, 120f, 30f), "Retry Level")) {
                    Application.LoadLevel(currentSceneIndex);

                }
            }
            if(GUI.Button(new Rect(Screen.width * 0.5f - 215f, Screen.height * 0.5f + 110f, 120f, 30f), "Main Menu")) {
                Application.LoadLevel(0);
            }
        }
        // Allow the user to press Escape to bring up a menu when the level hasn't finished yet
        else if(inGameMenu) {
            GUI.Box(new Rect(Screen.width * 0.5f - 80f, Screen.height * 0.5f - 90f, 160f, 180f), "");
            // Cancel
            if(GUI.Button(new Rect(Screen.width * 0.5f - 60f, Screen.height * 0.5f - 60f, 120f, 30f), "Resume")) {
                inGameMenu = false;
            }
            // Restart the current level
            if(GUI.Button(new Rect(Screen.width * 0.5f - 60f, Screen.height * 0.5f - 15f, 120f, 30f), "Restart Level")) {
                Application.LoadLevel(currentSceneIndex);
            }
            // Quit to Main Menu
            if(GUI.Button(new Rect(Screen.width * 0.5f - 60f, Screen.height * 0.5f + 30f, 120f, 30f), "Quit")) {
                Application.LoadLevel(0);
            }
        }
    }
}