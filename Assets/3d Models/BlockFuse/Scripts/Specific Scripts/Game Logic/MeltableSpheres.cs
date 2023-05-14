using System.Collections;
using UnityEngine;

public class MeltableSpheres : MonoBehaviour {
    private Transform trans; // Cache the transform component to increase performance
    private Rigidbody rb;
    [SerializeField]
    private float density = 5f; // Generic unit, density = initial mass / initial diametre

    private IEnumerator Start() {
        trans = transform;
        rb = GetComponent<Rigidbody>();

        while(true) {
            rb.mass = density * trans.localScale.x;
            yield return new WaitForSeconds(0.3f);
        }
    }
}