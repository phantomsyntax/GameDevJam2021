using TMPro;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class UserInterfaceManager : MonoBehaviour {
        [Header("User Interface Settings")]
        [SerializeField] private Canvas winLose;
        [SerializeField] private TextMeshProUGUI winLoseText;
        private bool bPlayerHasWon;
        
        
        // Start is called before the first frame update
        void Start()
        {
            // Null check for winLoseText;
            winLose.enabled = false;
            winLoseText.text = "";
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        public void ToggleWinLoseText(bool bPlayerHasWon) {
            if (bPlayerHasWon) {
                winLoseText.text = "You Win!";
            }
            else {
                winLoseText.text = "You Lose!";
            }

            winLose.enabled = true;
        }

        public void HandleRetryButtonClick() {
            print("--- Reload the scene");
        }

        public void HandleQuitButtonClick() {
            print("--- Go back to the Main Menu");
        }
    }
}
