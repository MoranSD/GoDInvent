using Gameplay.FightSystem.Health;
using Gameplay.FightSystem.Data;

namespace Gameplay.FightSystem
{
    public class EnemyAttackSystem
    {
        public EnemyHealth healthSystem { get; private set; }

        private ArmorType _targetArmor;

        private EnemyAttackConfig _config;

        public EnemyAttackSystem(EnemyAttackConfig config, EnemyHealth enemyHealth)
        {
            healthSystem = enemyHealth;

            _config = config;
        }

        public void OnAttack(PlayerAttackSystem playerAttack)
        {
            if (healthSystem.health <= 0) return;

            playerAttack.healthSystem.ApplyDamage(_config.damage, _targetArmor);
            UpdateArmorTarget();
        }
        private void UpdateArmorTarget()
        {
            _targetArmor = _targetArmor switch
            {
                ArmorType.head => ArmorType.torso,
                ArmorType.torso => ArmorType.head,
                _ => ArmorType.torso,
            };
        }
    }
}
