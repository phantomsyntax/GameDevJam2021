using TMPro;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class UpdateTextField : MonoBehaviour {
        [Header("Text Update Settings")]
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        public void NewText(string newText) {
            textMeshProUGUI.text = newText;
        }
    }
}
