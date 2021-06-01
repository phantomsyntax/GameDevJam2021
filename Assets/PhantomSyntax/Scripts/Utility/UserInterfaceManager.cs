using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomSyntax.Scripts.Utility {
    public class UserInterfaceManager : MonoBehaviour {
        [Header("User Interface Settings")]
        [SerializeField] private Canvas winLose;
        [SerializeField] private TextMeshProUGUI winLoseText;
        
        // Start is called before the first frame update
        void Start()
        {
            // Null check for winLoseText;
            winLose.enabled = false;
            winLoseText.text = "";
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
            SceneManager.LoadScene(1);
        }

        public void HandleQuitButtonClick() {
            SceneManager.LoadScene(0);
        }
    }
}
