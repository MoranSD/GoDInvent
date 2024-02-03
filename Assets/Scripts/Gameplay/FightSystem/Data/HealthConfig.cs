using UnityEngine;

namespace Gameplay.FightSystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/HealthConfig")]
    public class HealthConfig : ScriptableObject
    {
        public int maxCount;
        public int startCount;
    }
}
