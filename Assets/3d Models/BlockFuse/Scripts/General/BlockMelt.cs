using UnityEngine;
using UnityEngine.Serialization;

public class BlockMelt : MonoBehaviour {
    public GlobalLogic globalLogic;

    private static readonly Vector3 blockSize = new Vector3(0.21f, 0.15f, 0.63f);

    [SerializeField]
    [FormerlySerializedAs("speed")]
    internal float meltingSpeed = 0.005f; // The speed at which the block is melted
    [SerializeField]
    private float minSize = 0.0004f; // The minimum size the block can be before it is destroyed
    
    private void OnCollisionStay(Collision collision) {
        Transform trans = collision.transform;
        switch(trans.gameObject.layer) {
            case 8:
                // Layer 8 is used to identify a meltable block
                trans.localScale = MeltBlock(trans);

                /**
                * Check each component of the Vector3 to find out if they are less than minimum size.
                * The aim is to only worry about the block if it is visible, so if it's less than the
                * minimum size, then we don't want to perform any more instructions on the given block.
                */
                if(trans.localScale.x < minSize || trans.localScale.y < minSize || trans.localScale.z < minSize) {
                    DestroyObject(trans.gameObject);
                }
                break;
            case 11:
                // Layer 11 is used to identify a meltable sphere
                // Scale sphere until it completely melts
                trans.localScale = MeltSphere(trans.localScale);

                if(trans.localScale.x < minSize) {
                    DestroyObject(trans.gameObject);
                }
                break;
            case 12:
                // Layer 12 is for recycled meltable blocks (the rain blocks). These blocks are only disabled, not deleted
                trans.localScale = MeltBlock(trans);

                if(trans.localScale.x < minSize || trans.localScale.y < minSize || trans.localScale.z < minSize) {
                    trans.gameObject.SetActive(false);
                    // Reset the localScale of the block
                    trans.localScale = blockSize;
                }
                break;
        }
    }

    //  Returns the new size by incrementing the amount the block has been melted.
    internal Vector3 MeltBlock(Transform trans) {
        Vector3 dot = new Vector3(
            Mathf.Abs(Mathf.Round(Vector3.Dot(trans.right, Vector3.up))), 
            Mathf.Abs(Mathf.Round(Vector3.Dot(trans.up, Vector3.up))),
            Mathf.Abs(Mathf.Round(Vector3.Dot(trans.forward, Vector3.up))));

        return trans.localScale - dot * meltingSpeed; // Shrink the block based on the current axis it is resting upon
    }

    internal Vector3 MeltSphere(Vector3 size) {
        return size - new Vector3(meltingSpeed, meltingSpeed, meltingSpeed);
    }

    /**
	* Handle the logic for destroying the object. The object is question is also passed
	* to allow for specialized logic. Overrides BlockMeltBase.DestroyObject.
	*/
    internal void DestroyObject(GameObject gO) {
        if(globalLogic == null) return;

        int points;
        if(gO.layer == 8) points = 2; // Meltable block award
        else points = 1;
        // Reward the player for making blocks fall to the floor
        globalLogic.AddScore(points);
		Destroy(gO);
	}
}