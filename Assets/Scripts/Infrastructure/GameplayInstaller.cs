using Gameplay.FightSystem.Health;
using Gameplay.FightSystem.Data;
using Gameplay.FightSystem;
using Gameplay.Data;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private AttackConfig _attackConfig;
        [SerializeField] private EnemyAttackConfig _enemyAttackConfig;
        [SerializeField] private HealthConfig _playerHealthConfig;
        [SerializeField] private HealthConfig _enemyHealthConfig;
        [SerializeField] private PrizeGiverConfig _prizeGiverConfig;
        [SerializeField] private Gameplay.EndGameHandler _endGameHandler;
        public override void InstallBindings()
        {
            InstallHealthes();
            InstallExecutorFactory();
            InstallAttackSystems();
            InstallPrizeGiver();
            InstallEndGameHandler();
        }
        private void InstallHealthes()
        {
            Container.Bind<PlayerHealth>().AsSingle().WithArguments(_playerHealthConfig);
            Container.Bind<EnemyHealth>().AsSingle().WithArguments(_playerHealthConfig);
        }
        private void InstallExecutorFactory()
        {
            Container.Bind<Gameplay.UI.Popups.ItemPopupExecutorFactory>().AsSingle().WithArguments(Container);
        }
        private void InstallAttackSystems()
        {
            Container.Bind<PlayerAttackSystem>().AsSingle().WithArguments(_attackConfig);
            Container.BindInterfacesAndSelfTo<EnemyAttackSystem>().AsSingle().WithArguments(_enemyAttackConfig);
        }
        private void InstallEndGameHandler()
        {
            Container.Bind<Gameplay.EndGameHandler>().FromInstance(_endGameHandler).AsSingle();
        }
        private void InstallPrizeGiver()
        {
            Container.Bind<Gameplay.PrizeGiver>().AsSingle().WithArguments(_prizeGiverConfig);
        }
    }
}
