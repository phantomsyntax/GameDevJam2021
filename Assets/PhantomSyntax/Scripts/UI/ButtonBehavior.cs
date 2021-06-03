using UnityEngine;

namespace PhantomSyntax.Scripts.UI {
    [RequireComponent(typeof(AudioSource))]
    public class ButtonBehavior : MonoBehaviour {
        [Header("Audio Clip Settings")]
        [SerializeField] private AudioSource buttonAudioSource;
        [SerializeField] private AudioClip buttonSelectAudioClip;
        [SerializeField] private AudioClip buttonSubmitAudioClip;

        public void HandleSelectAudioClip() {
            buttonAudioSource.PlayOneShot(buttonSelectAudioClip, 1.0f);
        }
    
        public void HandleSubmitAudioClip() {
            buttonAudioSource.PlayOneShot(buttonSubmitAudioClip, 1.0f);
        }
    }
}
