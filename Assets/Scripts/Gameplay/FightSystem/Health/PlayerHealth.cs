using Gameplay.InventorySystem.Items;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.FightSystem.Health
{
    public class PlayerHealth : HealthSystem
    {
        public List<IArmor> equippedArmors { get; private set; } = new();

        InventorySystem.Data.ItemsDataHandler _itemsDataHandler;

        public PlayerHealth (Data.HealthConfig config, InventorySystem.Data.ItemsDataHandler itemsDataHandler) : base(config) 
        {
            _itemsDataHandler = itemsDataHandler;
        }
        public override void ApplyDamage(int damage, ArmorType armorType)
        {
            if (health <= 0) return;
            if (damage <= 0) return;

            if (HasArmor(armorType))
            {
                var armor = GetArmor(armorType);
                damage -= armor.armorPoints;
                if (damage <= 0) return;
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
        public void ResetArmor()
        {
            equippedArmors.Clear();
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
        public override object GetData()
        {
            return new PlayerHealthData()
            {
                health = health,
                equippedArmors = equippedArmors.Select(x=> x.armorType).ToList(),
            };
        }
        public override void SetData(object data)
        {
            var healthData = (PlayerHealthData)data;

            health = healthData.health;
            for (int i = 0; i < healthData.equippedArmors.Count; i++)
            {
                var armor = _itemsDataHandler
                    .CreateItem(
                    x => 
                    x is InventorySystem.Data.ItemArmorConfig armorConfig && 
                    armorConfig.armorType == healthData.equippedArmors[i]);

                SetArmor((IArmor)armor);
            }

            onChangedEvent?.Invoke();
        }
        private void RemoveArmor(ArmorType armorType)
        {
            if (HasArmor(armorType) == false) return;

            equippedArmors.Remove(GetArmor(armorType));
            onChangedEvent?.Invoke();
        }

        [System.Serializable]
        private struct PlayerHealthData
        {
            public int health;
            public List<ArmorType> equippedArmors;
        }
    }
}
