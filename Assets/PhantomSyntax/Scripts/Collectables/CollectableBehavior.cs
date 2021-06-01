using PhantomSyntax.Scripts.ScriptableObjects;
using UnityEngine;

namespace PhantomSyntax.Scripts.Collectables {
    public class CollectableBehavior : MonoBehaviour {
        [Header("Token Collection Settings")]
        [SerializeField] private Collider tokenCollider;
        [SerializeField] private ParticleSystem tokenCollectParticle;
        [SerializeField] private GameEvent tokenCollectedEvent;
        [SerializeField] private IntegerValue tokensCollected;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!tokenCollider) {
                gameObject.GetComponent<Collider>();
            }
            // Null check for particle
        }

        // Update is called once per frame
        void Update()
        {
            // Rotate the token
            gameObject.transform.Rotate(Vector3.right);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                Instantiate(tokenCollectParticle, transform.position, transform.rotation);

                ++tokensCollected.Value;
                // Trigger GameEvent to update UI text
                tokenCollectedEvent.Triggered();
                
                gameObject.SetActive(false);
                Destroy(gameObject, 1.0f);
            }
        }
    }
}
