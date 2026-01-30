using UnityEngine;
using UnityEngine.Events;
using InteractionSystem.Core;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Toggle-based switch or lever that emits an event
    /// when its state changes.
    /// </summary>
    public class SwitchInteractable : MonoBehaviour, IInteractable
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Maximum interaction distance.")]
        private float m_MaxInteractionRange = 2.0f;

        [SerializeField]
        [Tooltip("Optional interaction point.")]
        private Transform m_InteractionPoint;

        [SerializeField]
        [Tooltip("Invoked when the switch is toggled. True = On, False = Off.")]
        private UnityEvent<bool> m_OnSwitchToggled;

        private bool m_IsOn;
        #endregion

        #region IInteractable Properties
        /// <summary>
        /// Transform used as the interaction point.
        /// </summary>
        public Transform InteractionPoint =>
            m_InteractionPoint != null ? m_InteractionPoint : transform;

        /// <summary>
        /// Maximum interaction range.
        /// </summary>
        public float MaxInteractionRange => m_MaxInteractionRange;
        #endregion

        #region IInteractable Methods
        /// <summary>
        /// Determines whether the switch can be interacted with.
        /// </summary>
        public bool CanInteract(in InteractionContext context, out string failReason)
        {
            failReason = null;
            return true;
        }

        /// <summary>
        /// Returns the interaction prompt for the switch.
        /// </summary>
        public string GetInteractionPrompt(in InteractionContext context)
        {
            return m_IsOn
                ? "Press E to Turn Off"
                : "Press E to Turn On";
        }

        /// <summary>
        /// Toggles the switch state and emits the toggle event.
        /// </summary>
        public void BeginInteraction(in InteractionContext context)
        {
            Toggle();
        }

        /// <summary>
        /// Toggle interaction does not require per-frame updates.
        /// </summary>
        public void UpdateInteraction(in InteractionContext context) { }

        /// <summary>
        /// No cleanup required for toggle interaction.
        /// </summary>
        public void EndInteraction(in InteractionContext context) { }
        #endregion

        #region Private Methods
        private void Toggle()
        {
            m_IsOn = !m_IsOn;
            m_OnSwitchToggled?.Invoke(m_IsOn);
        }
        #endregion
    }
}
