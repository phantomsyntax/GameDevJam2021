using PhantomSyntax.Scripts.Utility;
using UnityEngine;

namespace PhantomSyntax.Scripts.Checkpoints {
    public class CheckpointBehavior : MonoBehaviour {
        [Header("Checkpoint Spawn Settings")]
        [SerializeField] private float checkpointSpeed = 5.0f;
        private Collider checkpointCollider;
        private SpawnObjects objectSpawner;

        private void Start() {
            checkpointCollider = GetComponent<Collider>();
            objectSpawner = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnObjects>();
            if (!objectSpawner) {
                print("[CheckpointBehavior] - Check SpawnManager object for proper Tag");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Kill object when it's past the player position/offscreen
            if (gameObject.transform.position.z < -5.0f) {
                Destroy(gameObject);
            }

            float deltaSpeed = (Time.deltaTime * checkpointSpeed);
            // Have to use up vector instead of forward due to object rotation (could be fixed at the object level)
            gameObject.transform.Translate(Vector3.up * deltaSpeed);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                objectSpawner.UpdateCheckpointUI();
            }
        }
    }
}
