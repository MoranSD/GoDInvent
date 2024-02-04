using Gameplay.InventorySystem.Items;
using Gameplay.FightSystem.Health;
using Gameplay.FightSystem.Data;
using Gameplay.InventorySystem;
using System;

namespace Gameplay.FightSystem
{
    public class PlayerAttackSystem
    {
        public event Action onChangeWeaponEvent;

        public PlayerHealth healthSystem { get; private set; }
        public WeaponType currentWeapon { get; private set; } = WeaponType.pistol;

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
            currentWeapon = type;
            onChangeWeaponEvent?.Invoke();
        }
        public void Attack()
        {
            if (healthSystem.health <= 0 || _enemyAttack.healthSystem.health <= 0) return;

            int shotsCount = _config.GetRequiredBulletsCount(currentWeapon);
            var requiredBulletType = _config.GetRequiredBulletType(currentWeapon);

            if (_inventory.TryGetItemStack(x => x.item is IBullet bullet && bullet.type == requiredBulletType && x.item.count >= shotsCount, out var bulletStack))
            {
                if (bulletStack.TryGet(shotsCount) == false) return;
            }
            else
            {
                return;
            }

            int damage = _config.GetDamage(currentWeapon);
            for (int i = 0; i < shotsCount; i++)
            {
                bool lastAttack = _enemyAttack.healthSystem.health <= damage;

                _enemyAttack.healthSystem.ApplyDamage(damage, _targetArmor);
                UpdateArmorTarget();

                if (lastAttack) return;
            }

            _enemyAttack.OnAttack(this);
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
