using System;
using UnityEngine;

namespace PhantomSyntax.Scripts.Camera {
    public class FollowPointBehavior : MonoBehaviour {
        [Header("Animator Settings")]
        [SerializeField] private Animator followPointAnimator;

        public void RotateToFront(bool bPlayerHasWon) {
            if (bPlayerHasWon) {
                followPointAnimator.SetTrigger("PlayerHasWon");
            }
        }
    }
}
