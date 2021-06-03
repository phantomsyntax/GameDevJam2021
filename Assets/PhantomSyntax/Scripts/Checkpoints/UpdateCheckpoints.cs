using System;
using System.Collections.Generic;
using PhantomSyntax.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomSyntax.Scripts.Checkpoints {
    [RequireComponent(typeof(AudioSource))]
    public class UpdateCheckpoints : MonoBehaviour
    {
        [Header("Checkpoint Update Settings")]
        [SerializeField] private List<Image> checkpointIndicators;
        [SerializeField] private IntegerValue checkpointsPassed;
        
        [ColorUsage(true, true)]
        [SerializeField] private Color highlightColor = Color.green;

        [Header("Checkpoint Audio Settings")]
        [SerializeField] private AudioSource checkpointAudioSource;
        [SerializeField] private AudioClip checkpointPassedAudioClip;

        private void Awake() {
            if (!checkpointAudioSource) {
                checkpointAudioSource = GetComponent<AudioSource>();
            }
        }

        public void Highlight() {
            if (checkpointsPassed.Value <= checkpointIndicators.Count) {
                checkpointIndicators[checkpointsPassed.Value - 1].color = highlightColor;
            }
        }

        public void Animate() {
            if (checkpointsPassed.Value <= checkpointIndicators.Count) {
                checkpointIndicators[checkpointsPassed.Value - 1].GetComponent<Animator>().Play("CheckpointAnimate");
            }
        }

        public void TriggerAudioClip() {
            checkpointAudioSource.PlayOneShot(checkpointPassedAudioClip, 1.0f);
        }
    }
}