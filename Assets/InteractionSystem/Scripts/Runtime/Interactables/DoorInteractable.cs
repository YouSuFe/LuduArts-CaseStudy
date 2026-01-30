using UnityEngine;
using InteractionSystem.Core;
using InteractionSystem.Inventory;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Toggleable door interactable that can optionally require a key item.
    /// </summary>
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Optional key required to open this door. If null, door is unlocked.")]
        private ItemDefinition m_RequiredKey;

        [SerializeField]
        [Tooltip("Maximum distance at which this door can be interacted with.")]
        private float m_MaxInteractionRange = 2.0f;

        [SerializeField]
        [Tooltip("Optional interaction point for UI and distance checks.")]
        private Transform m_InteractionPoint;

        [SerializeField]
        [Tooltip("Local rotation when the door is open.")]
        private Vector3 m_OpenRotationEuler = new Vector3(0f, 90f, 0f);

        private bool m_IsOpen;
        private Quaternion m_ClosedRotation;
        #endregion

        #region IInteractable Properties
        public Transform InteractionPoint =>
            m_InteractionPoint != null ? m_InteractionPoint : transform;

        public float MaxInteractionRange => m_MaxInteractionRange;
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            m_ClosedRotation = transform.localRotation;
        }
        #endregion

        #region IInteractable Methods
        public bool CanInteract(in InteractionContext context, out string failReason)
        {
            // If door is locked, check inventory
            if (m_RequiredKey != null && !m_IsOpen)
            {
                if (!context.InteractorTransform.TryGetComponent(out PlayerInventory inventory) ||
                    !inventory.HasItem(m_RequiredKey))
                {
                    failReason = "Key Required";
                    return false;
                }
            }

            failReason = null;
            return true;
        }

        public string GetInteractionPrompt(in InteractionContext context)
        {
            return m_IsOpen ? "Press E to Close" : "Press E to Open";
        }

        public void BeginInteraction(in InteractionContext context)
        {
            ToggleDoor();
        }

        public void UpdateInteraction(in InteractionContext context)
        {
            // Toggle interaction – nothing to update per frame
        }

        public void EndInteraction(in InteractionContext context)
        {
            // No cleanup required
        }
        #endregion

        #region Private Methods
        private void ToggleDoor()
        {
            m_IsOpen = !m_IsOpen;

            transform.localRotation = m_IsOpen
                ? Quaternion.Euler(m_OpenRotationEuler)
                : m_ClosedRotation;
        }
        #endregion
    }
}
