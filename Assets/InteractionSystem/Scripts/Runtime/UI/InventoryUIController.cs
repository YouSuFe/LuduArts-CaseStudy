using UnityEngine;
using UnityEngine.UI;
using InteractionSystem.Inventory;
using System.Collections.Generic;

namespace InteractionSystem.UI
{
    /// <summary>
    /// Displays a simple inventory UI showing collected items.
    /// </summary>
    public class InventoryUIController : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Reference to the player's inventory.")]
        private PlayerInventory m_PlayerInventory;

        [SerializeField]
        [Tooltip("UI image slots used to display inventory items.")]
        private Image[] m_ItemSlots;
        #endregion

        #region Unity Lifecycle
        private void OnEnable()
        {
            if (m_PlayerInventory != null)
            {
                m_PlayerInventory.OnInventoryChanged += RefreshUI;
            }

            RefreshUI();
        }

        private void OnDestroy()
        {
            if (m_PlayerInventory != null)
            {
                m_PlayerInventory.OnInventoryChanged -= RefreshUI;
            }
        }
        #endregion

        #region Private Methods
        private void RefreshUI()
        {
            for (int i = 0; i < m_ItemSlots.Length; i++)
            {
                m_ItemSlots[i].enabled = false;
            }

            if (m_PlayerInventory == null)
            {
                return;
            }

            IReadOnlyList<ItemDefinition> items = m_PlayerInventory.Items;

            for (int i = 0; i < items.Count && i < m_ItemSlots.Length; i++)
            {
                // For now, we simply enable the slot.
                // Icon support can be added later.
                m_ItemSlots[i].enabled = true;
            }
        }
        #endregion
    }
}
