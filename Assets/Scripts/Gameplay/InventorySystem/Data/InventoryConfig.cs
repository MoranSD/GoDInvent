using UnityEngine;

namespace Gameplay.InventorySystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/InventoryConfig")]
    public class InventoryConfig : ScriptableObject
    {
        public Vector2Int size;
        public ItemInInvetory[] startItems;

        [System.Serializable]
        public struct ItemInInvetory
        {
            public ItemConfig itemConfig;
            public Vector2Int position;
        }
    }
}
