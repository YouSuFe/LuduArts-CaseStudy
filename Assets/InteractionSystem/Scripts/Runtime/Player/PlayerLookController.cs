using UnityEngine;

namespace InteractionSystem.Player
{
    /// <summary>
    /// Handles mouse look for first-person camera control.
    /// </summary>
    public class PlayerLookController : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Mouse sensitivity multiplier.")]
        private float m_MouseSensitivity = 100f;

        [SerializeField]
        [Tooltip("Reference to the player body transform.")]
        private Transform m_PlayerBody;

        private float m_XRotation;
        #endregion

        #region Unity Lifecycle
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            HandleMouseLook();
        }
        #endregion

        #region Private Methods
        private void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

            m_XRotation -= mouseY;
            m_XRotation = Mathf.Clamp(m_XRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(m_XRotation, 0f, 0f);
            m_PlayerBody.Rotate(Vector3.up * mouseX);
        }
        #endregion
    }
}
