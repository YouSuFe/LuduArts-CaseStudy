using UnityEngine;
using InteractionSystem.Core;
using InteractionSystem.Inventory;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Instant interactable that adds a key item to the player's inventory.
    /// </summary>
    public class KeyPickupInteractable : MonoBehaviour, IInteractable
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Item definition added to the inventory when picked up.")]
        private ItemDefinition m_ItemDefinition;

        [SerializeField]
        [Tooltip("Maximum distance at which this key can be picked up.")]
        private float m_MaxInteractionRange = 2.0f;

        [SerializeField]
        [Tooltip("Optional custom interaction point.")]
        private Transform m_InteractionPoint;
        #endregion

        #region IInteractable Properties
        public Transform InteractionPoint =>
            m_InteractionPoint != null ? m_InteractionPoint : transform;

        public float MaxInteractionRange => m_MaxInteractionRange;
        #endregion

        #region IInteractable Methods
        public bool CanInteract(in InteractionContext context, out string failReason)
        {
            failReason = null;
            return true;
        }

        public string GetInteractionPrompt(in InteractionContext context)
        {
            return "Press E to Pick Up";
        }

        public void BeginInteraction(in InteractionContext context)
        {
            if (!context.InteractorTransform.TryGetComponent(out PlayerInventory inventory))
            {
                Debug.LogWarning("PlayerInventory not found on player.");
                return;
            }

            inventory.AddItem(m_ItemDefinition);
            gameObject.SetActive(false);
        }

        public void UpdateInteraction(in InteractionContext context) { }

        public void EndInteraction(in InteractionContext context) { }
        #endregion
    }
}
