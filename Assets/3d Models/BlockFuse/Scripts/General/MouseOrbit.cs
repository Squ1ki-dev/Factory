//  Modified Standard Asset
using UnityEngine;

public class MouseOrbit : MonoBehaviour {
    public static MouseOrbit Instance;

    private const float xSpeed = 250f;
    private const float ySpeed = 120f;
    private const float xKeySpeed = 150f;
    private const float yKeySpeed = 75f;
    private const float panYSpeed = 2.5f;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private float yMinLimit = -5; // Number of degrees the camera can orbit vertically
    [SerializeField]
    private float yMaxLimit = 70;

    private float panY = 0f; // Units the Y-Axis has been moved
    private Vector3 position;
    private float lastThrowTime = 0f; // The time is seconds the last time the projectile was fired
    private const float projectileCooldownTime = 1f;

    [SerializeField]
    private float projectileVelocity = 6.248f; // Projectile Velocity
    [SerializeField]
    internal string projectileTypeLabel = "Ball"; // Used to communicate the projectile type with the HUD

    private float x = 0f;
    private float y = 0f;
    private Transform cachedTransform;
    
    public GlobalLogic globalLogic; // Used for keeping track of the throws remaining

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        // Update the type incase it is something other than a ball
        projectileTypeLabel = projectile.name;

        // Cache a few components to squeeze out a little more performance
        cachedTransform = transform;
        position = cachedTransform.position;
        var angles = cachedTransform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void Update() {
        Quaternion rotation;

        // Check if the user in pressive LeftAlt or LeftControl
        if(Input.GetKey(KeyCode.LeftControl)) {
            // Check if the user is pushing Alt+LMB to check if its right to orbit the camera
            if(Input.GetMouseButton(0)) {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            }
        }

        /**
        * If the user wasn't pressing LeftAlt, but did click the LMB, shoot the projectile.
        * Ignore this check if the level is finished, to not only prevent the user from 
        * shooting again, and also to ensure the FinishedLevel function is only fired once
        */
        else if(Input.GetMouseButtonDown(0)) {
            if(globalLogic.finished == false && globalLogic.throws > 0) {
                /**
	    	    * Check if the in-game menu is active, if so, don't allow any projectiles to be fired.
	    	    * Otherwise this will cause annoyances such as the user firing a ball when clicking resume
	    	    */
                if(GlobalLogic.Instance.inGameMenu == false) {
                    // Get two samples of where the mouse is in world space at varying distances
                    Vector3 p1 = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
                    Vector3 p2 = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));

                    // Instantiate the projectile
                    GameObject spawned = (GameObject)Instantiate(projectile, p1, transform.rotation);
                    // By subtracting both positions, we get the direction the projectile should travel at
                    spawned.GetComponent<Rigidbody>().velocity = (p2 - p1) * projectileVelocity;

                    // Strategy/Patience Bonus
                    if(Time.timeSinceLevelLoad > lastThrowTime + projectileCooldownTime) {
                        // Award a bonus of up to 1.5 to reward the player from playing slowly
                        GlobalLogic.Instance.strategyBonus += Mathf.Clamp(Time.timeSinceLevelLoad - (lastThrowTime +
                            projectileCooldownTime), 0f, 1.5f);
                    }
                    // Take one throw off the remaining
                    globalLogic.throws--;
                    lastThrowTime = Time.timeSinceLevelLoad;
                    if(globalLogic.throws == 0) {
                        /**
		        	    * Inform the GlobalLogic script that the current game has finished. This changes 
		        	    * many lines of code related to the GUI
		        	    */
                        StartCoroutine(globalLogic.FinishedLevel());
                    }
                }
            }
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            panY += Input.GetAxis("Vertical") * panYSpeed * Time.deltaTime;
            panY = Mathf.Clamp(panY, -1.2f, 2.25f);
        } else {
            // Update Movement, and watch out for keyboard use
            x -= Input.GetAxis("Horizontal") * xKeySpeed * Time.deltaTime;
            y += Input.GetAxis("Vertical") * yKeySpeed * Time.deltaTime;

            rotation = Quaternion.Euler(y, x, 0f);
            position = rotation * new Vector3(0f, 0f, -distance) + target.position;
            cachedTransform.rotation = rotation;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }

        // Pan on the Y-Axis with MMB, or by holing Shift and pusing the horizontal keys
        if(Input.GetMouseButton(2)) {
            panY += (Input.GetAxis("Mouse Y") + Input.GetAxis("Vertical")) * panYSpeed * Time.deltaTime;
            panY = Mathf.Clamp(panY, -1.2f, 2.25f);
        }

        cachedTransform.position = new Vector3(position.x, position.y + panY, position.z);
    }

    private static float ClampAngle(float angle, float min, float max) {
        if(angle < -360f)
            angle += 360f;
        else if(angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}