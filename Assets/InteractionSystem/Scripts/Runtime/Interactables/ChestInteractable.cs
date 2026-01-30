using UnityEngine;
using InteractionSystem.Core;
using InteractionSystem.Inventory;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Interactable chest that requires holding the interaction input
    /// for a specified duration to open. Can optionally require a key.
    /// Can only be opened once.
    /// </summary>
    public class ChestInteractable : MonoBehaviour, IInteractable
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Optional key required to open this chest. If null, chest is unlocked.")]
        private ItemDefinition m_RequiredKey;

        [SerializeField, Range(0.1f, 10f)]
        [Tooltip("Time (in seconds) the interaction key must be held to open the chest.")]
        private float m_HoldDuration = 2.0f;

        [SerializeField]
        [Tooltip("Item added to the inventory when the chest is opened.")]
        private ItemDefinition m_ContainedItem;

        [SerializeField, Range(0.1f, 10f)]
        [Tooltip("Maximum distance at which this chest can be interacted with.")]
        private float m_MaxInteractionRange = 2.0f;

        [SerializeField]
        [Tooltip("Optional interaction point used for distance checks and UI.")]
        private Transform m_InteractionPoint;

        private float m_CurrentHoldTime;
        private bool m_IsOpened;
        #endregion

        #region Public Properties
        /// <summary>
        /// Current accumulated hold time. Used for hold progress feedback.
        /// </summary>
        public float CurrentHoldTime => m_CurrentHoldTime;

        /// <summary>
        /// Required hold duration to open the chest.
        /// </summary>
        public float HoldDuration => m_HoldDuration;

        /// <summary>
        /// Indicates whether the chest has already been opened.
        /// </summary>
        public bool IsOpened => m_IsOpened;

        #endregion

        #region IInteractable Properties
        /// <summary>
        /// Transform used as the interaction point.
        /// </summary>
        public Transform InteractionPoint =>
            m_InteractionPoint != null ? m_InteractionPoint : transform;

        /// <summary>
        /// Maximum interaction distance.
        /// </summary>
        public float MaxInteractionRange => m_MaxInteractionRange;
        #endregion

        #region IInteractable Methods
        /// <summary>
        /// Determines whether the chest can be interacted with.
        /// Blocks interaction if already opened, or if a required key is missing.
        /// </summary>
        public bool CanInteract(in InteractionContext context, out string failReason)
        {
            if (m_IsOpened)
            {
                failReason = "Chest already opened";
                return false;
            }

            if (m_RequiredKey != null)
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
        /// Returns the interaction prompt displayed to the user.
        /// </summary>
        public string GetInteractionPrompt(in InteractionContext context)
        {
            if (m_IsOpened)
            {
                return string.Empty;
            }

            return m_RequiredKey != null
                ? "Hold E to Unlock Chest"
                : "Hold E to Open Chest";
        }

        /// <summary>
        /// Called when the interaction begins. Resets hold progress.
        /// </summary>
        public void BeginInteraction(in InteractionContext context)
        {
            m_CurrentHoldTime = 0f;
        }

        /// <summary>
        /// Called every frame while the interaction is active.
        /// Accumulates hold time and opens the chest when completed.
        /// </summary>
        public void UpdateInteraction(in InteractionContext context)
        {
            if (m_IsOpened || !context.IsHeld)
            {
                return;
            }

            m_CurrentHoldTime += context.DeltaTime;

            if (m_CurrentHoldTime >= m_HoldDuration)
            {
                OpenChest(context);
            }
        }

        /// <summary>
        /// Called when the interaction ends or is cancelled.
        /// Resets hold progress if the chest is not opened.
        /// </summary>
        public void EndInteraction(in InteractionContext context)
        {
            if (!m_IsOpened)
            {
                m_CurrentHoldTime = 0f;
            }
        }
        #endregion

        #region Private Methods
        private void OpenChest(in InteractionContext context)
        {
            m_IsOpened = true;

            if (!context.InteractorTransform.TryGetComponent(out PlayerInventory inventory))
            {
                Debug.LogError(
                    $"[{nameof(ChestInteractable)}] PlayerInventory not found on interactor.",
                    this);
                return;
            }
            inventory.AddItem(m_ContainedItem);

            Debug.Log($"Chest opened: {name}");
        }
        #endregion
    }
}
