using UnityEngine;

namespace Gameplay.FightSystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/EnemyAttackConfig")]
    public class EnemyAttackConfig : ScriptableObject
    {
        public int maxHealth;
        public int startHealth;
        public int damage;
    }
}
