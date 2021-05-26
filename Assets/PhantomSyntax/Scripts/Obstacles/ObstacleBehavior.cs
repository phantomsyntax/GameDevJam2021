using UnityEditor.UIElements;
using UnityEngine;

namespace PhantomSyntax.Scripts.Obstacles {
    public class ObstacleBehavior : MonoBehaviour
    {
        [Header("Obstacle Movement Settings")]
        [SerializeField] private float objectSpeedMin = 2.5f;
        [SerializeField] private float objectSpeedMax = 6.0f;
        private float objectSpeed;
        
        // Start is called before the first frame update
        void Start() {
            objectSpeed = Random.Range(objectSpeedMin, objectSpeedMax);
        }

        // Update is called once per frame
        void Update() // TODO: change to object pooling
        {
            // Kill object when it's past the player position/offscreen
            if (gameObject.transform.position.z < -5.0f) {
                Destroy(gameObject);
            }

            float deltaSpeed = (Time.deltaTime * objectSpeed);
            gameObject.transform.Translate(Vector3.forward * deltaSpeed);
        }
    }
}
