namespace Gameplay.FightSystem.Health
{
    public class EnemyHealth : HealthSystem
    {
        public EnemyHealth(Data.HealthConfig config) : base(config) { }

        public override object GetData() => health;

        public override void SetData(object data)
        {
            health = (int)data;
            onChangedEvent?.Invoke();
        }
    }
}
