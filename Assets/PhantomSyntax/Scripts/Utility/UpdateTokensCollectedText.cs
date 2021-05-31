using PhantomSyntax.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class UpdateTokensCollectedText : MonoBehaviour {
        [Header("Text Update Settings")]
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private IntegerValue tokensCollected;

        public void NewValue() {
            textMeshProUGUI.text = tokensCollected.Value.ToString();
        }
    }
}