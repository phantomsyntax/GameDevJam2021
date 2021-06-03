using PhantomSyntax.Scripts.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PhantomSyntax.Scripts.Obstacles {
    public class ObstacleBehavior : MonoBehaviour
    {
        [Header("Obstacle Movement Settings")]
        [SerializeField] private float objectSpeedMin = 2.5f;
        [SerializeField] private float objectSpeedMax = 6.0f;
        private float objectSpeed;

        [Header("Spawn Manager Attachment")]
        [SerializeReference] private SpawnObjects _spawnObjects;
        
        // Start is called before the first frame update
        void Start() {
            objectSpeed = Random.Range(objectSpeedMin, objectSpeedMax);
            AttachSpawnManager();
        }

        // Update is called once per frame
        void Update() // TODO: change to object pooling
        {
            // Kill object when it's past the player position/offscreen
            if (gameObject.transform.position.z < -5.0f) {
                Destroy(gameObject, 2.0f);
            }

            float deltaSpeed = (Time.deltaTime * objectSpeed);
            gameObject.transform.Translate(Vector3.forward * deltaSpeed);
        }

        public void AttachSpawnManager() {
            GameObject spawnManager = GameObject.Find("SpawnManager");
            _spawnObjects = spawnManager.GetComponent<SpawnObjects>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                _spawnObjects.bPlayerHasWon = false;
            }
        }
    }
}
