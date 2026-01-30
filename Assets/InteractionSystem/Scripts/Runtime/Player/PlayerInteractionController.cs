using UnityEngine;
using InteractionSystem.Core;

namespace InteractionSystem.Player
{
    /// <summary>
    /// Handles player input and drives the interaction lifecycle
    /// for the currently detected interactable.
    /// </summary>
    [RequireComponent(typeof(InteractionDetector))]
    public class PlayerInteractionController : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Key used to trigger interactions.")]
        private KeyCode m_InteractionKey = KeyCode.E;

        private InteractionDetector m_InteractionDetector;

        private IInteractable m_ActiveInteractable;
        // Caches last detected interactable to avoid missing interactions
        // due to throttled detection vs frame-based input timing.
        private IInteractable m_LastDetectedInteractable;
        private bool m_IsInteracting;
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            m_InteractionDetector = GetComponent<InteractionDetector>();
        }

        private void Update()
        {
            HandleInteraction();
        }
        #endregion

        #region Private Methods
        private void HandleInteraction()
        {
            bool wasPressed = Input.GetKeyDown(m_InteractionKey);
            bool isHeld = Input.GetKey(m_InteractionKey);
            bool wasReleased = Input.GetKeyUp(m_InteractionKey);

            InteractionContext context = new InteractionContext(
                transform,
                Time.deltaTime,
                wasPressed,
                isHeld,
                wasReleased);

            // Cache last detected interactable to avoid timing issues
            IInteractable detected = m_InteractionDetector.CurrentInteractable;
            if (detected != null)
            {
                m_LastDetectedInteractable = detected;
            }

            if (m_IsInteracting)
            {
                UpdateActiveInteraction(context);
                return;
            }

            if (!wasPressed || m_LastDetectedInteractable == null)
            {
                return;
            }

            if (!m_LastDetectedInteractable.CanInteract(context, out _))
            {
                return;
            }

            m_ActiveInteractable = m_LastDetectedInteractable;
            m_IsInteracting = true;

            m_ActiveInteractable.BeginInteraction(context);
        }

        private void UpdateActiveInteraction(in InteractionContext context)
        {
            if (m_ActiveInteractable == null)
            {
                m_IsInteracting = false;
                return;
            }

            m_ActiveInteractable.UpdateInteraction(context);

            if (context.WasReleasedThisFrame)
            {
                m_ActiveInteractable.EndInteraction(context);
                m_ActiveInteractable = null;
                m_IsInteracting = false;
            }
        }
        #endregion
    }
}
