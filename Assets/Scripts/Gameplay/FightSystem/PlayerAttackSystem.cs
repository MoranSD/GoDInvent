using Gameplay.InventorySystem.Items;
using Gameplay.FightSystem.Health;
using Gameplay.FightSystem.Data;
using Gameplay.InventorySystem;
using System;

namespace Gameplay.FightSystem
{
    public class PlayerAttackSystem
    {
        public event Action onAttackEvent;

        public PlayerHealth healthSystem { get; private set; }

        private WeaponType _currentWeapon;
        private ArmorType _targetArmor;

        private AttackConfig _config;
        private EnemyAttackSystem _enemyAttack;
        private Inventory _inventory;

        public PlayerAttackSystem(AttackConfig config, EnemyAttackSystem enemyAttack, PlayerHealth playerHealth, Inventory inventory)
        {
            healthSystem = playerHealth;

            _config = config;
            _enemyAttack = enemyAttack;
            _inventory = inventory;
        }

        public void SetWeapon(WeaponType type)
        {
            _currentWeapon = type;
        }
        public void Attack()
        {
            int shotsCount = _config.GetRequiredBulletsCount(_currentWeapon);
            var requiredBulletType = _config.GetRequiredBulletType(_currentWeapon);

            if (_inventory.TryGetItemStack(x => x.item is IBullet bullet && bullet.type == requiredBulletType && x.item.count >= shotsCount, out var bulletStack))
            {
                if (bulletStack.TryGet(shotsCount) == false) return;
            }
            else
            {
                return;
            }

            int damage = _config.GetDamage(_currentWeapon);
            for (int i = 0; i < shotsCount; i++)
            {
                bool lastAttack = _enemyAttack.healthSystem.health <= damage;

                _enemyAttack.healthSystem.ApplyDamage(damage, _targetArmor);
                UpdateArmorTarget();

                if (lastAttack) return;
            }

            onAttackEvent?.Invoke();
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
