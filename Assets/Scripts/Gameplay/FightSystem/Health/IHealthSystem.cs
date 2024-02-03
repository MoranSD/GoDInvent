namespace Gameplay.FightSystem.Health
{
    public interface IHealthSystem
    {
        public int health { get; }
        public void ApplyDamage(int damage, ArmorType armorType);
    }
}
