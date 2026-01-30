using UnityEngine;

namespace InteractionSystem.Player
{
    /// <summary>
    /// Handles basic WASD movement using CharacterController.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Private Fields
        [SerializeField, Range(1f, 10f)]
        [Tooltip("Movement speed in units per second.")]
        private float m_MoveSpeed = 5f;

        [SerializeField, Range(-30f, -1f)]
        [Tooltip("Gravity force applied to the player.")]
        private float m_Gravity = -9.81f;

        private CharacterController m_CharacterController;
        private Vector3 m_Velocity;
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            ApplyGravity();
        }
        #endregion

        #region Private Methods
        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move =
                transform.right * horizontal +
                transform.forward * vertical;

            m_CharacterController.Move(
                move * m_MoveSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (m_CharacterController.isGrounded && m_Velocity.y < 0f)
            {
                m_Velocity.y = -2f;
            }

            m_Velocity.y += m_Gravity * Time.deltaTime;
            m_CharacterController.Move(m_Velocity * Time.deltaTime);
        }
        #endregion
    }
}
