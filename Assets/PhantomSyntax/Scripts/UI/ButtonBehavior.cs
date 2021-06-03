using UnityEngine;

namespace PhantomSyntax.Scripts.UI {
    [RequireComponent(typeof(AudioSource))]
    public class ButtonBehavior : MonoBehaviour {
        [Header("Audio Clip Settings")]
        [SerializeField] private AudioSource buttonAudioSource;
        [SerializeField] private AudioClip buttonSelectAudioClip;
        [SerializeField] private AudioClip buttonSubmitAudioClip;

        public void HandleSelectAudioClip() {
            buttonAudioSource.clip = buttonSelectAudioClip;
            buttonAudioSource.Play(); 
        }
    
        public void HandleSubmitAudioClip() {
            buttonAudioSource.clip = buttonSubmitAudioClip;
            buttonAudioSource.Play();
        }
    }
}
