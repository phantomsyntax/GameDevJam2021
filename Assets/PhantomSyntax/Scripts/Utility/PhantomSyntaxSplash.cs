using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace PhantomSyntax.Scripts.Utility {
    public class PhantomSyntaxSplash : MonoBehaviour {
        [Header("Splash Screen Text Settings")]
        [SerializeField] private TextMeshProUGUI splashScreenText;
        [SerializeField] private string splashScreenTextstring = "PHANTOM::SYNTAX";
        [SerializeField] private float typingSpeed = 0.4f;

        [Header("Splash Screen Audio Settings")]
        [SerializeField] private AudioSource splashScreenAudioSource;
        [SerializeField] private AudioClip[] typingAudioClip;

        private void Awake() {
            splashScreenText.text = String.Empty;
        }

        private void Start() {
            StartCoroutine(nameof(TypeOutName));
        }

        private IEnumerator TypeOutName() {
            foreach (char character in splashScreenTextstring) {
                splashScreenAudioSource.PlayOneShot(typingAudioClip[Random.Range(0, typingAudioClip.Length)]);
                splashScreenText.text += character;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(2.0f);
            StartCoroutine(nameof(FadeOutText));
        }

        private IEnumerator FadeOutText() {
            while (splashScreenText.color.a > 0) {
                Color color = splashScreenText.color;
                color.a -= 0.1f;
                splashScreenText.color = new Color(splashScreenText.color.r, splashScreenText.color.g, splashScreenText.color.b, color.a);
                yield return new WaitForSeconds(0.01f);
            }

            SceneManager.LoadScene(1);
        }
    }
}
