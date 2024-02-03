using Gameplay.FightSystem.Health;
using Gameplay.FightSystem.Data;
using Zenject;

namespace Gameplay.FightSystem
{
    public class EnemyAttackSystem : ILateDisposable
    {
        public EnemyHealth healthSystem { get; private set; }

        private ArmorType _targetArmor;

        private EnemyAttackConfig _config;
        private PlayerAttackSystem _playerAttack;

        public EnemyAttackSystem(EnemyAttackConfig config, EnemyHealth enemyHealth)
        {
            healthSystem = enemyHealth;

            _config = config;
        }
        [Inject]
        private void Initialize(PlayerAttackSystem playerAttack)
        {
            _playerAttack = playerAttack;
            _playerAttack.onAttackEvent += OnAttack;
        }
        public void LateDispose()
        {
            _playerAttack.onAttackEvent -= OnAttack;
        }

        private void OnAttack()
        {
            if (healthSystem.health <= 0) return;

            _playerAttack.healthSystem.ApplyDamage(_config.damage, _targetArmor);
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
