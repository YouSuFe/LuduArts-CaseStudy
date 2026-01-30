using UnityEngine;

namespace InteractionSystem.Inventory
{
    /// <summary>
    /// Defines a collectible item that can be stored in the inventory.
    /// </summary>
    [CreateAssetMenu(
        fileName = "Item_",
        menuName = "InteractionSystem/Inventory/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Unique identifier used for logic and comparisons.")]
        private string m_ItemId;

        [SerializeField]
        [Tooltip("Display name shown in UI.")]
        private string m_DisplayName;

        [SerializeField]
        [Tooltip("Icon used for UI representation.")]
        private Sprite m_Icon;
        #endregion

        #region Public Properties
        /// <summary>
        /// Unique item identifier.
        /// </summary>
        public string ItemId => m_ItemId;

        /// <summary>
        /// Display name for UI.
        /// </summary>
        public string DisplayName => m_DisplayName;

        /// <summary>
        /// Icon used in inventory UI.
        /// </summary>
        public Sprite Icon => m_Icon;
        #endregion
    }
}
