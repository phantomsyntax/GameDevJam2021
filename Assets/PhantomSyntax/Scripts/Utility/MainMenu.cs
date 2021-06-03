using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomSyntax.Scripts.Utility {
    public class MainMenu : MonoBehaviour {

        public void HandleStartButtonClick() {
            StartCoroutine(nameof(LoadSceneDelayed));
        }

        public void HandleExitButtonClick() {
            StartCoroutine(nameof(ExitGameDelayed));
        }
        
        private IEnumerator LoadSceneDelayed() {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(1);
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
