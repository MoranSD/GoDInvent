using Gameplay.InventorySystem.Items;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.FightSystem.Health
{
    public class PlayerHealth : HealthSystem
    {
        public List<IArmor> equippedArmors { get; private set; } = new();

        public PlayerHealth (Data.HealthConfig config) : base(config) { }
        public override void ApplyDamage(int damage, ArmorType armorType)
        {
            if (health <= 0) return;
            if (damage <= 0) return;

            if (HasArmor(armorType))
            {
                var armor = GetArmor(armorType) as ArmorItem;
                if(armor.armorPoints >= damage)
                {
                    damage = 0;
                    armor.armorPoints -= damage;
                }
                else
                {
                    damage -= armor.armorPoints;
                    armor.armorPoints = 0;
                }

                if(armor.armorPoints <= 0)
                    RemoveArmor(armorType);
            }

            health -= damage;
            onChangedEvent?.Invoke();

            if (health <= 0) 
                onDieEvent?.Invoke();
        }
        public void Heal(int healthPoints)
        {
            if (healthPoints <= 0) return;

            health = System.Math.Min(health + healthPoints, maxHealth);
            onChangedEvent?.Invoke();
        }
        public bool HasArmor(ArmorType armorType) => equippedArmors.Any(x => x.armorType == armorType);
        public IArmor GetArmor(ArmorType armorType) => equippedArmors.First(x => x.armorType == armorType);
        public void SetArmor(IArmor armor)
        {
            if (HasArmor(armor.armorType))
                RemoveArmor(armor.armorType);

            equippedArmors.Add(armor);
            onChangedEvent?.Invoke();
        }
        private void RemoveArmor(ArmorType armorType)
        {
            if (HasArmor(armorType) == false) return;

            equippedArmors.Remove(GetArmor(armorType));
            onChangedEvent?.Invoke();
        }
    }
}
