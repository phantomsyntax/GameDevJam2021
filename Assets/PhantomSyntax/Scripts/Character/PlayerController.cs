using UnityEngine;
using UnityEngine.InputSystem;

namespace PhantomSyntax.Scripts.Character {
    public class PlayerController : MonoBehaviour {
        [Header("World Settings")]
        [SerializeField] private float gravityValue = -9.81f;

        [Header("Player Movement Settings")]
        [SerializeField] private CharacterController playerCharacterController;
        [SerializeField] private float playerMovementSpeed = 5.0f;
        [SerializeField] private float playerJumpHeight = 1.0f;
        private Vector3 playerColliderCenter;
        private Vector3 playerVelocity;
        private Vector2 playerInputVector;
        private float playerColliderHeight;
        private bool bIsGrounded;

        [Header("Player Animation Settings")]
        [SerializeField] private Animator playerCharacterAnimator;

        [Header("Event Settings")]
        [SerializeField] private GameObject playerFollowPoint;

        private void Awake() {
            // Null checks
            if (!playerCharacterController) {
                playerCharacterController = GetComponent<CharacterController>();
            }
            if (!playerCharacterAnimator) {
                playerCharacterAnimator = GetComponent<Animator>();
            }

            // Cache the collider sizes for crouching
            playerColliderHeight = playerCharacterController.height;
            playerColliderCenter = playerCharacterController.center;

        }

        // Update is called once per frame
        void Update() {
            bIsGrounded = playerCharacterController.isGrounded;
            // Avoid negative velocity.y values
            if (bIsGrounded && playerVelocity.y < 0.0f) {
                playerVelocity.y = 0.0f;
            }

            // Grab input vector from HandleOnMove() and apply it times player speed
            if (bIsGrounded) {
                Vector3 movementVector = new Vector3(playerInputVector.x, 0.0f, 0.0f);
                float deltaSpeed = playerMovementSpeed * Time.deltaTime;
                playerCharacterController.Move(movementVector * deltaSpeed);
            }

            // Grab velocity from HandleOnJump() and trigger jump logic            
            playerVelocity.y += gravityValue * Time.deltaTime;
            playerCharacterController.Move(playerVelocity * Time.deltaTime);
        }

        public void ChangeInputMap() {
            // Switches current InputSystem map to disable player movement
            GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        }
        
        public void TriggerCameraRotation(bool value) { 
            // Rotate the player's FollowPoint to get the CM to chase it
            // TODO: slerp the rotation so it slows down
            if (value) {
                playerFollowPoint.transform.Rotate(new Vector3(-26.0f, 160.0f, 0.0f));
            }
        }

        public void HandleOnMove(InputAction.CallbackContext value) {
            playerInputVector = value.ReadValue<Vector2>();

            // Handle skating forward and crouching animations
            if (playerInputVector.y > 0.0f && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsSkatingForward", true);
            }
            else if (playerInputVector.y < 0.0f && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsCrouching", true);
                
                // Adjust the Collider size for crouching
                playerCharacterController.height = playerColliderHeight * 0.5f;
                playerCharacterController.center = playerColliderCenter * 0.5f;
            }
            else if (playerInputVector.x == 0.0f && playerInputVector.y == 0.0f) {
                playerCharacterAnimator.SetBool("bIsSkatingForward", false);
                playerCharacterAnimator.SetBool("bIsCrouching", false);
                playerCharacterController.height = playerColliderHeight;
                playerCharacterController.center = playerColliderCenter;
            }

            // Handle side-skating animation
            if (playerInputVector.x != 0.0f && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsFreeSkating", true);
            }
            else {
                playerCharacterAnimator.SetBool("bIsFreeSkating", false);
            }
        }

        public void HandleOnJump(InputAction.CallbackContext value) {
            if (value.started && bIsGrounded) {
                playerCharacterAnimator.SetBool("bIsJumping", true);
                playerVelocity.y += Mathf.Sqrt(playerJumpHeight * -3.0f * gravityValue);
            }

            if (value.canceled) {
                playerCharacterAnimator.SetBool("bIsJumping", false);
            }
        }
    }
}