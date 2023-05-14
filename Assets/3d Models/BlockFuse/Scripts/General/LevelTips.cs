using System.Collections;
using UnityEngine;

public class LevelTips : MonoBehaviour {
    [SerializeField]
    private string levelTip;
    [SerializeField]
    private GUISkin skin;
    [SerializeField]
    private int lineCount = 1;

    private const int padding = 12; // Padding of the GUISkin's Box
    private const int textAreaLineSizeY = 12; // Font size of the GUISkin's Box
    private int textAreaSizeY;
    private int textAreaPosY;

    private Color fadeColor;

    private float beginFade = 7.5f;

    private IEnumerator Start() {
        var paddingTotal = padding * 2;
        textAreaSizeY = paddingTotal + (lineCount * textAreaLineSizeY);
        textAreaPosY = Screen.height - textAreaSizeY + 6; // The extra 6 pixels is used to keep the GUI.Box slightly cut off for an added effect

        beginFade += lineCount * 2f; // Automatically increment the fade time by 1 second per line

        /**
	    * If time has exceeded 1 second and is below 2 seconds, it is time to fade in the box
	    * Incrementing the alpha component by Time.deltaTime causes the fade to take 1 second
	    */
        yield return new WaitForSeconds(1f);
        // Continue fading the alpha until its completely solid
        while(fadeColor.a < 1f) {
            fadeColor.a = Mathf.Min(Time.deltaTime + fadeColor.a, 1f);
            yield return null;
        }

        /**
	    * Sleep until beginFade, in this case we need to subtract two seconds, because it takes 
	    * that long for it to arrive at 7 seconds elapsed
	    */
        yield return new WaitForSeconds(beginFade - 2f);
        // Fade the alpha channel until its completely transparent
        while(fadeColor.a > 0f) {
            fadeColor.a = Mathf.Max(fadeColor.a - Time.deltaTime, 0f);
            yield return null;
        }
        // After its completely faded, destroy the script because it's no longer necessary
        Destroy(this);
    }

    private void OnGUI() {
        GUI.skin = skin;
        GUI.color = fadeColor;
        GUI.Box(new Rect(Screen.width * 0.5f - 200f, textAreaPosY, 400f, textAreaSizeY), levelTip);
    }
}