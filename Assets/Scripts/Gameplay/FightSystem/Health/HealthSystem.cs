namespace Gameplay.FightSystem.Health
{
    public abstract class HealthSystem : IHealthSystem
    {
        public static System.Action onChangedEvent;
        public static System.Action onDieEvent;

        public int health { get; protected set; }
        public int maxHealth { get; protected set; }

        public HealthSystem(Data.HealthConfig config)
        {
            health = System.Math.Min(config.startCount, config.maxCount);
            maxHealth = config.maxCount;
        }

        public virtual void ApplyDamage(int damage, ArmorType armorType)
        {
            if (health <= 0) return;
            if (damage <= 0) return;

            health -= damage;
            onChangedEvent?.Invoke();

            if (health <= 0)
                onDieEvent?.Invoke();
        }
        public void ResetHealth()
        {
            health = maxHealth;
            onChangedEvent?.Invoke();
        }
    }
}
