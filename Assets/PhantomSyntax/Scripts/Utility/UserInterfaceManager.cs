using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace PhantomSyntax.Scripts.Utility {
    public class UserInterfaceManager : MonoBehaviour {
        [Header("User Interface Settings")]
        [SerializeField] private EventSystem winLoseEventSystem;
        [SerializeField] private Canvas winLose;
        [SerializeField] private TextMeshProUGUI winLoseText;

        [Header("Transition Settings")]
        [SerializeField] private Animator crossfadeAnimator;

        // Start is called before the first frame update
        void Start()
        {
            // Null check for winLoseText;
            winLose.enabled = false;
            winLoseText.text = "";
            winLoseEventSystem.sendNavigationEvents = false;
        }
    
        public void ToggleWinLoseText(bool bPlayerHasWon) {
            if (bPlayerHasWon) {
                winLoseText.text = "You Win!";
            }
            else {
                winLoseText.text = "You Lose!";
            }

            winLoseEventSystem.sendNavigationEvents = true;
            winLose.enabled = true;
        }

        public void HandleRetryButtonClick() {
            crossfadeAnimator.SetTrigger("Crossfade");
            StartCoroutine("LoadSceneDelayed", 2);
        }

        public void HandleQuitButtonClick() {
            crossfadeAnimator.SetTrigger("Crossfade");
            StartCoroutine("LoadSceneDelayed", 1);
        }

        private IEnumerator LoadSceneDelayed(object sceneIndex) {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene((int)sceneIndex);
        }
    }
}
