using System;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem.Inventory
{
    /// <summary>
    /// Runtime inventory that stores collected items for the player.
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        #region Private Fields
        private readonly HashSet<string> m_ItemIds = new HashSet<string>();
        private readonly List<ItemDefinition> m_Items = new List<ItemDefinition>();
        #endregion

        #region Events
        /// <summary>
        /// Invoked whenever the inventory content changes.
        /// </summary>
        public event Action OnInventoryChanged;
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns a read-only list of items in the inventory.
        /// </summary>
        public IReadOnlyList<ItemDefinition> Items => m_Items;
        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        public void AddItem(ItemDefinition item)
        {
            if (item == null)
            {
                Debug.LogWarning("Tried to add null item to inventory.");
                return;
            }

            if (m_ItemIds.Add(item.ItemId))
            {
                m_Items.Add(item);
                Debug.Log($"Item added to inventory: {item.DisplayName}");
                OnInventoryChanged?.Invoke();
            }
        }


        /// <summary>
        /// Checks whether the inventory contains the given item.
        /// </summary>
        /// <param name="item">Item definition to check for.</param>
        /// <returns>
        /// True if the item exists in the inventory; otherwise false.
        /// </returns>
        public bool HasItem(ItemDefinition item)
        {
            if (item == null)
            {
                Debug.LogWarning(
                    $"[{nameof(PlayerInventory)}] HasItem called with null item.",
                    this);
                return false;
            }

            return m_ItemIds.Contains(item.ItemId);
        }

        #endregion
    }
}
