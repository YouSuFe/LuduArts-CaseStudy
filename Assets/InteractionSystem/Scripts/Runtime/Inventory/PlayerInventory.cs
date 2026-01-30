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

            m_ItemIds.Add(item.ItemId);
            Debug.Log($"Item added to inventory: {item.DisplayName}");
        }

        /// <summary>
        /// Checks whether the inventory contains the given item.
        /// </summary>
        public bool HasItem(ItemDefinition item)
        {
            return item != null && m_ItemIds.Contains(item.ItemId);
        }
        #endregion
    }
}
