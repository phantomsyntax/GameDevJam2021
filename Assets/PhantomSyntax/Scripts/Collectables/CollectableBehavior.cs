using System;
using PhantomSyntax.Scripts.Utility;
using UnityEngine;

namespace PhantomSyntax.Scripts.Collectables {
    public class CollectableBehavior : MonoBehaviour {
        [Header("Token Collection Settings")]
        [SerializeField] private Collider tokenCollider;
        [SerializeField] private ParticleSystem tokenCollectParticle;
        private SpawnObjects objectSpawner;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!tokenCollider) {
                gameObject.GetComponent<Collider>();
            }
            
            objectSpawner = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnObjects>();
            if (!objectSpawner) {
                print("[CheckpointBehavior] - Check SpawnManager object for proper Tag");
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
            print(other.tag);
            if (other.gameObject.CompareTag("Player")) {
                Instantiate(tokenCollectParticle, transform.position, transform.rotation);
                objectSpawner.UpdateTokenUI();
                gameObject.SetActive(false);
                Destroy(gameObject, 1.0f);
            }
        }
    }
}
