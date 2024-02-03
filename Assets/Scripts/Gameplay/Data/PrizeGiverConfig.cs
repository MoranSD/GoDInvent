using Gameplay.InventorySystem.Data;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Game/Data/PrizeGiverConfig")]
    public class PrizeGiverConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _randomPrizes;
        public ItemConfig GetRandomPrize()
        {
            return _randomPrizes[Random.Range(0, _randomPrizes.Length)];
        }
    }
}
