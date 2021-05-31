using PhantomSyntax.Scripts.ScriptableObjects;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class GameManager : MonoBehaviour {
        [Header("Game Managers")]
        [SerializeField] private GameObject SpawnManager;
        [SerializeField] private GameObject UserInterfaceManager;
        [SerializeField] private IntegerValue tokensCollected;
        
        // Start is called before the first frame update
        void Start()
        {
            // Null checks for the managers
            tokensCollected.Value = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // TODO: move the main loop logic from SpawnObjects over here
        }
    }
}
