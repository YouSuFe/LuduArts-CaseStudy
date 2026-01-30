using UnityEngine;

namespace InteractionSystem.Inventory
{
    /// <summary>
    /// Defines a collectible item using a ScriptableObject.
    /// This is pure data and contains no runtime state.
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewItemDefinition",
        menuName = "Interaction System/Inventory/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        #region Private Fields
        [SerializeField]
        [Tooltip("Unique identifier for this item.")]
        private string m_ItemId;

        [SerializeField]
        [Tooltip("Display name shown to the player.")]
        private string m_DisplayName;
        #endregion

        #region Public Properties
        public string ItemId => m_ItemId;
        public string DisplayName => m_DisplayName;
        #endregion
    }
}
