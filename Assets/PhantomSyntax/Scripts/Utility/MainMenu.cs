using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomSyntax.Scripts.Utility {
    public class MainMenu : MonoBehaviour {

        public void HandleStartButtonClick() {
            SceneManager.LoadScene(1);
        }

        public void HandleExitButtonClick() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
