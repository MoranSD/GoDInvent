using UnityEngine;
using System.Linq;

namespace Gameplay.FightSystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/AttackConfig")]
    public class AttackConfig : ScriptableObject
    {
        public int maxHealth;
        public int startHealth;

        [SerializeField] private BulletsPerAttack[] _bulletsPerAttack;
        [SerializeField] private WeaponDamage[] _weaponsDamage;
        [SerializeField] private WeaponBullet[] _weaponBullets;

        public int GetRequiredBulletsCount(WeaponType weaponType)
        {
            return _bulletsPerAttack.First(x => x.weaponType == weaponType).count;
        }
        public int GetDamage(WeaponType weaponType)
        {
            return _weaponsDamage.First(x => x.weaponType == weaponType).damage;
        }
        public BulletType GetRequiredBulletType(WeaponType weaponType)
        {
            return _weaponBullets.First(x => x.weaponType == weaponType).bulletType;
        }

        [System.Serializable]
        private struct BulletsPerAttack
        {
            public WeaponType weaponType;
            public int count;
        }
        [System.Serializable]
        private struct WeaponDamage
        {
            public WeaponType weaponType;
            public int damage;
        }
        [System.Serializable]
        private struct WeaponBullet
        {
            public WeaponType weaponType;
            public BulletType bulletType;
        }
    }
}
