using PhantomSyntax.Scripts.ScriptableObjects;
using UnityEngine;

namespace PhantomSyntax.Scripts.Collectables {
    [RequireComponent(typeof(AudioSource))]
    public class CollectableBehavior : MonoBehaviour {
        [Header("Token Collection Settings")]
        [SerializeField] private Collider tokenCollider;
        [SerializeField] private ParticleSystem tokenCollectParticle;
        [SerializeField] private GameEvent tokenCollectedEvent;
        [SerializeField] private IntegerValue tokensCollected;
        [SerializeField] private GameObject coinBlank;

        [Header("Token AudioClip Settings")]
        [SerializeField] private AudioSource tokenAudioSource;
        [SerializeField] private AudioClip tokenCollectAudioClip;
        [SerializeField] private AudioClip tokenShockwaveAudioClip;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!tokenCollider) {
                gameObject.GetComponent<Collider>();
            }

            if (!tokenAudioSource) {
                tokenAudioSource = GetComponent<AudioSource>();
            }
            
            // Null check for particle
        }

        // Update is called once per frame
        void Update()
        {
            // Rotate the token
            gameObject.transform.Rotate(Vector3.right);
        }

        private void TokenCollectionSounds() {
            tokenAudioSource.PlayOneShot(tokenCollectAudioClip);
            tokenAudioSource.PlayOneShot(tokenShockwaveAudioClip);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                // Instantiate the particle
                Instantiate(tokenCollectParticle, transform.position, transform.rotation);
                // Trigger the collect SFX
                TokenCollectionSounds();
                
                ++tokensCollected.Value;
                // Trigger GameEvent to update UI text
                tokenCollectedEvent.Triggered();
                // Delete the coin sprite
                Destroy(coinBlank);
            }
        }
    }
}
