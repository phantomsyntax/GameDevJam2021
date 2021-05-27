using UnityEngine;

namespace PhantomSyntax.Scripts.Checkpoints {
    public class CheckpointBehavior : MonoBehaviour {
        
        [SerializeField] private float checkpointSpeed = 5.0f;        

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
    }
}
