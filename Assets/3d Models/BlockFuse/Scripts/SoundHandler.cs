using UnityEngine;

public class SoundHandler : MonoBehaviour {
    [SerializeField]
    private float collisionMultiplier = 0.022f; // Used to lower the overall volume of the impact
    [SerializeField]
    private float playThreshold = 0.04f; // The minimum amount of volume that is allowed to be played
    [SerializeField]
    private float maxVolume = 1f;

    private AudioSource cachedAudioSource;
    private Rigidbody cachedRigidbody;

    void Start() {
        cachedAudioSource = GetComponent<AudioSource>();
        cachedRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision) {
        cachedAudioSource.volume = Mathf.Min(cachedRigidbody.velocity.magnitude * collisionMultiplier, maxVolume);

        if(cachedAudioSource.volume > playThreshold && cachedAudioSource.enabled) {
            cachedAudioSource.Play();
        }
    }
}