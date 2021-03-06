using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomSyntax.Scripts.Utility {
    public class MainMenu : MonoBehaviour {
        [Header("Info Button Settings")]
        [SerializeField] private Canvas infoCanvas;
        
        [Header("Transition Settings")]
        [SerializeField] private Animator crossfadeAnimator;

        private void Awake() {
            if (!infoCanvas) {
                infoCanvas.gameObject.SetActive(false);
            }
        }

        public void HandleStartButtonClick() {
            crossfadeAnimator.SetTrigger("Crossfade");
            StartCoroutine(nameof(LoadSceneDelayed));
        }

        public void HandleExitButtonClick() {
            crossfadeAnimator.SetTrigger("Crossfade");
            StartCoroutine(nameof(ExitGameDelayed));
        }

        public void HandleInfoButtonClick() {
            infoCanvas.gameObject.SetActive(true);
        }

        public void HandleCloseInfoButtonClick() {
            infoCanvas.gameObject.SetActive(false);
        }
        
        private IEnumerator LoadSceneDelayed() {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(2);
        }

        private IEnumerator ExitGameDelayed() {
            yield return new WaitForSeconds(1.0f);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
