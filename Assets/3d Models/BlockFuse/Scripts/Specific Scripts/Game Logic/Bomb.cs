using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField]
    private float explosionForce = 4500f;
    [SerializeField]
    private float explosionRadius = 1f;

    private void OnCollisionEnter() {
        Vector3 pos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(pos, explosionRadius);
        foreach(Collider hit in colliders) {
            if(!hit) continue;

            if(hit.GetComponent<Rigidbody>()) {
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, pos, explosionRadius, 0f);
            }
        }
        Destroy(gameObject);
    }
}