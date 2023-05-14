using UnityEngine;

public class Destructible : MonoBehaviour {
    [SerializeField]
    private float maxImpact = 5f;
    [SerializeField]
    private GameObject replacement;

    private void OnCollisionEnter(Collision collision) {
        float impactForce = collision.relativeVelocity.magnitude * collision.rigidbody.mass;

        if(impactForce >= maxImpact) {
            replacement.SetActive(true);

            Destroy(gameObject);
        }
    }
}