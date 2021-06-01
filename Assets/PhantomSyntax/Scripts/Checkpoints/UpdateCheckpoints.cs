using System.Collections.Generic;
using PhantomSyntax.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomSyntax.Scripts.Checkpoints {
    public class UpdateCheckpoints : MonoBehaviour
    {
        [Header("Checkpoint Update Settings")]
        [SerializeField] private List<Image> checkpointIndicators;
        [SerializeField] private IntegerValue checkpointsPassed;
        
        [ColorUsage(true, true)]
        [SerializeField] private Color highlightColor = Color.green;
        public void Highlight() {
            if (checkpointsPassed.Value <= checkpointIndicators.Count) {
                checkpointIndicators[checkpointsPassed.Value - 1].color = highlightColor;  
            }
        }
    }
}