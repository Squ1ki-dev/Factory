using System.Collections;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour {
	private int meltMode = 0;
    private GameObject[] blocks;
    private Rigidbody[] blockRigidbodies;

    [SerializeField]
    private Rigidbody[] blueBlocks;
    [SerializeField]
    private BlockMelt blockMelt;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Color[] colours;
    
    private Material[] colourMaterials; // Used to store runtime created materials based on the "colours" variable
    
    private void Awake() {
        StartCoroutine(DoBlockRain());

        Shader diffuseShader = Shader.Find("Standard");

        // Create the colour materials so the blocks can be batch rendered by sharing the same material
        colourMaterials = new Material[colours.Length];
        
        for(int c = 0; c < colours.Length; c++) {
            colourMaterials[c] = new Material(diffuseShader);
            colourMaterials[c].color = colours[c];
        }

        // Initialize the blocks that will be raining down onto the floor
        blocks = new GameObject[100];
        blockRigidbodies = new Rigidbody[100];
        for(int b = 0; b < blocks.Length; b++) {
            blocks[b] = (GameObject)Instantiate(blockPrefab, Vector3.zero, Random.rotation);
            blocks[b].SetActive(false);
            
            // Assign a random material to the block
            blocks[b].transform.GetChild(0).GetComponent<MeshRenderer>().material = colourMaterials[Random.Range(0, colours.Length)];

            blockRigidbodies[b] = blocks[b].GetComponent<Rigidbody>();

            blockRigidbodies[b].sleepThreshold = 0;
        }
    }

    private IEnumerator Start() {
        blockMelt.meltingSpeed = 0;

        yield return new WaitForSeconds(2f); // Delay the melting of the blocks
        
        // Wake up the rigidbodies of the blue blocks
        foreach(Rigidbody blueBlock in blueBlocks) {
            blueBlock.WakeUp();
        }

        // Begin increasing the melting speed of the blocks
        while(blockMelt.meltingSpeed < 0.0055f) {
            yield return new WaitForFixedUpdate();
            blockMelt.meltingSpeed += 0.000007f;
        }

        //  Wait a bit before we enable different melt modes
        yield return new WaitForSeconds(3f);

        while(true) {
            yield return new WaitForFixedUpdate();
            switch(meltMode) {
                // Default melting mode
                case 0:
                    // If the time is divisible by 35, then it's time to melt in reverse
                    if(Time.time % 35 == 0) {
                        meltMode = 1;
                    }
                    /**
                    * When the time is something that is definetely not divisble by 35 (eg; 51.1), then it's time to
                    * stop melting for a short while
                    */
                    else if(Time.time % 51.1 == 0) {
                        meltMode = 2;
                    }
                    break;

                // Melt in reverse
                case 1:
                    // Decrease speed into a negative number to make the melting enlarge the objects instead
                    while(blockMelt.meltingSpeed > -0.005f) {
                        blockMelt.meltingSpeed -= 0.00005f;
                        yield return new WaitForFixedUpdate();
                    }
                    yield return new WaitForSeconds(1.5f);
                    // Begin returning to normal
                    while(blockMelt.meltingSpeed < 0.005f) {
                        blockMelt.meltingSpeed += 0.00005f;
                    }
                    meltMode = 0; // Back to default melting mode
                    break;
                // Stop melting
                case 2:
                    blockMelt.meltingSpeed = 0f; // Stop the melting process
                    yield return new WaitForSeconds(4f); // Delay for 4 seconds
                    blockMelt.meltingSpeed = 0.0055f; // Begin melting again at the usual rate
                    meltMode = 0;
                    break;
            }
        }
    }

    private IEnumerator DoBlockRain() {
        // Wait a bit after the initial structure has melted before raining blocks
        yield return new WaitForSeconds(11f);

        Vector3 newPosition;
        Vector3 centerOfSpawn = new Vector3(0f, 12f, 0f);

        // Make it rain blocks!
        while(true) {
            // Look for a block that is currently not enabled
            for(int i = 0; i < 100; i++) {
                if(blocks[i].activeSelf == true) continue;

                newPosition = centerOfSpawn + Random.insideUnitSphere * 6f;

                blocks[i].transform.rotation = Random.rotation;

                // Add some random force and torque to the block
                blockRigidbodies[i].AddForce(Random.insideUnitSphere * 3f, ForceMode.Acceleration);
                blockRigidbodies[i].AddTorque(Random.insideUnitSphere * 10f, ForceMode.Acceleration);

                blocks[i].transform.position = newPosition;
                blocks[i].SetActive(true);

                break;
            }
            yield return new WaitForSeconds(0.025f);
        }
    }
}