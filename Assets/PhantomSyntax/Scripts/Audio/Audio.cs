using System.Collections;
using UnityEngine;

namespace PhantomSyntax.Scripts.Audio {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    
    public class Audio : MonoBehaviour {
        [Header("Audio Clip Settings")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;
        private float audioClipVolume = 1.0f;

        [Header("Level Complete Settings")]
        [SerializeField] private Animator audioSourceAnimator;
        
        private void Start() {
            if (!audioSource) {
                audioSource = GetComponent<AudioSource>();
            }

            if (!audioSourceAnimator) {
                audioSourceAnimator = GetComponent<Animator>();
            }
            
            if (audioClips.Length != 0) {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            else {
                Debug.LogWarning("[Audio] - No AudioClips loaded!");
            }
        }

        public void AudioTransition(bool bPlayerHasWon) {
            if (bPlayerHasWon) {
                StartCoroutine(nameof(HandleWinMusicTransition));
            }
            else {
                StartCoroutine(nameof(HandleLoseMusicTransition));
            }
        }

        public void FadeOutAudioClip() {
            audioSourceAnimator.Play("AudioClipFadeOut");
        }

        private IEnumerator HandleWinMusicTransition() {
            // Trigger Audio Animator fade
            audioSourceAnimator.SetTrigger("FadeOutAudio");
            yield return new WaitForSeconds(1.0f);
            // Trigger new audio clip and play
            audioSource.clip = audioClips[1];
            audioSource.volume = audioClipVolume;
            audioSource.Play();

            // TODO: trigger the new clip from the Animator event at end
        }

        private IEnumerator HandleLoseMusicTransition() {
            print("--- There is no Lose music yet");
            yield break;
        }
    }
}
