using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PhantomSyntax.Scripts.Character {
    public class PlayerController : MonoBehaviour {
        [Header("World Settings")] [SerializeField]
        private float gravityValue = -9.81f;

        [Header("Player Movement Settings")] [SerializeField]
        private CharacterController playerCharacterController;

        [SerializeField] private float playerMovementSpeed = 5.0f;
        [SerializeField] private float playerJumpHeight = 1.0f;
        private Vector3 m_playerVelocity;
        private Vector2 m_playerInputVector;
        private bool bIsGrounded;

        [Header("Player Animation Settings")] [SerializeField]
        private Animator playerCharacterAnimator;

        private void Awake() {
            if (!playerCharacterController) {
                playerCharacterController = GetComponent<CharacterController>();
            }

            if (!playerCharacterAnimator) {
                playerCharacterAnimator = GetComponent<Animator>();
            }
        }

        // Start is called before the first frame update
        void Start() {
        }

        // Update is called once per frame
        void Update() {
            bIsGrounded = playerCharacterController.isGrounded;
            // Avoid negative velocity.y values
            if (bIsGrounded && m_playerVelocity.y < 0.0f) {
                m_playerVelocity.y = 0.0f;
            }

            // Grab input vector from HandleOnMove() and apply it times player speed
            if (bIsGrounded) {
                Vector3 movementVector = new Vector3(m_playerInputVector.x, 0.0f, 0.0f);
                float deltaSpeed = playerMovementSpeed * Time.deltaTime;
                playerCharacterController.Move(movementVector * deltaSpeed);
            }

            // Grab velocity from HandleOnJump() and trigger jump logic            
            m_playerVelocity.y += gravityValue * Time.deltaTime;
            playerCharacterController.Move(m_playerVelocity * Time.deltaTime);
        }

        public void HandleOnMove(InputAction.CallbackContext value) {
            m_playerInputVector = value.ReadValue<Vector2>();

            // Handle skating forward and crouching animations
            if (m_playerInputVector.y > 0.0f && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsSkatingForward", true);
            }
            else if (m_playerInputVector.y < 0.0f && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsCrouching", true);
            }
            else {
                playerCharacterAnimator.SetBool("bIsSkatingForward", false);
                playerCharacterAnimator.SetBool("bIsCrouching", false);
            }

            // Handle side-skating animation
            if (m_playerInputVector.x != 0.0f && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsFreeSkating", true);
            }
            else {
                playerCharacterAnimator.SetBool("bIsFreeSkating", false);
            }
        }

        public void HandleOnJump(InputAction.CallbackContext value) {
            if (value.started && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsJumping", true);
                m_playerVelocity.y += Mathf.Sqrt(playerJumpHeight * -3.0f * gravityValue);
            }

            if (value.canceled) {
                playerCharacterAnimator.SetBool("bIsJumping", false);
            }
        }

        private void OnTriggerEnter(Collider other) {
        }
    }
}