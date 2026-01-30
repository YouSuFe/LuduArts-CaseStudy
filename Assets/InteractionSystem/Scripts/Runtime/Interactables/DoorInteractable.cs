using UnityEngine;
using InteractionSystem.Core;
using InteractionSystem.Inventory;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Interactable door that can be opened either by direct interaction
    /// (optionally requiring a key) or by an external trigger such as a switch.
    /// </summary>
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        #region Enums
        /// <summary>
        /// Defines how the door can be opened.
        /// </summary>
        public enum DoorOpenMode
        {
            DirectInteraction,
            ExternalTrigger
        }
        #endregion

        #region Private Fields
        [SerializeField]
        [Tooltip("Defines how this door is opened.")]
        private DoorOpenMode m_OpenMode = DoorOpenMode.DirectInteraction;

        [SerializeField]
        [Tooltip("Optional key required to open this door when using direct interaction.")]
        private ItemDefinition m_RequiredKey;

        [SerializeField]
        [Tooltip("Maximum distance at which this door can be interacted with.")]
        private float m_MaxInteractionRange = 2.0f;

        [SerializeField]
        [Tooltip("Optional interaction point.")]
        private Transform m_InteractionPoint;

        [SerializeField]
        [Tooltip("Local rotation when the door is open.")]
        private Vector3 m_OpenRotationEuler = new Vector3(0f, 90f, 0f);

        private bool m_IsOpen;
        private Quaternion m_ClosedRotation;
        #endregion

        #region Public Properties
        /// <summary>
        /// Indicates whether the door is currently open.
        /// </summary>
        public bool IsOpen => m_IsOpen;
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

        #region Unity Lifecycle
        private void Awake()
        {
            m_ClosedRotation = transform.localRotation;
        }
        #endregion

        #region IInteractable Methods
        /// <summary>
        /// Determines whether the door can be interacted with directly.
        /// Blocks interaction if a required key is missing and the door is closed.
        /// If it does not need key, and DoorOpenMode is not ExternalTrigger, it can be opened directly.
        /// </summary>
        public bool CanInteract(in InteractionContext context, out string failReason)
        {
            if (m_OpenMode == DoorOpenMode.ExternalTrigger)
            {
                failReason = "Use the switch";
                return false;
            }

            if (m_RequiredKey != null && !m_IsOpen)
            {
                if (!context.InteractorTransform.TryGetComponent(out PlayerInventory inventory) ||
                    !inventory.HasItem(m_RequiredKey))
                {
                    failReason = $"{m_RequiredKey.DisplayName} Required";
                    return false;
                }
            }

            failReason = null;
            return true;
        }

        /// <summary>
        /// Returns the interaction prompt for the door.
        /// </summary>
        public string GetInteractionPrompt(in InteractionContext context)
        {
            if (m_OpenMode == DoorOpenMode.ExternalTrigger)
            {
                return string.Empty;
            }

            return m_IsOpen
                ? "Press E to Close"
                : "Press E to Open";
        }

        /// <summary>
        /// Toggles the door when directly interacted with.
        /// </summary>
        public void BeginInteraction(in InteractionContext context)
        {
            ToggleDoor();
        }

        /// <summary>
        /// Toggle interaction does not require per-frame updates.
        /// </summary>
        public void UpdateInteraction(in InteractionContext context)
        {
            // Intentionally left empty.
        }

        /// <summary>
        /// No cleanup required for toggle interaction.
        /// </summary>
        public void EndInteraction(in InteractionContext context)
        {
            // No cleanup required for toggle interaction.
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the door open state externally (e.g., from a switch).
        /// </summary>
        public void SetOpenState(bool isOpen)
        {
            if (m_IsOpen == isOpen)
            {
                return;
            }

            m_IsOpen = isOpen;

            transform.localRotation = m_IsOpen
                ? Quaternion.Euler(m_OpenRotationEuler)
                : m_ClosedRotation;
        }
        #endregion

        #region Private Methods
        private void ToggleDoor()
        {
            SetOpenState(!m_IsOpen);
        }
        #endregion
    }
}
