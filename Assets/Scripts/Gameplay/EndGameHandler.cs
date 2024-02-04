using Gameplay.FightSystem.Health;
using Gameplay.InventorySystem;
using Infrastructure.Save;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class EndGameHandler : MonoBehaviour
    {
        private PlayerHealth _playerHealthSystem;
        private EnemyHealth _enemyHealthSystem;
        private Inventory _inventory;
        private PrizeGiver _prizeGiver;
        private ISaveSystem _saveSystem;

        [Inject]
        private void Construct(PlayerHealth playerHealth, EnemyHealth enemyHealth, Inventory inventory, PrizeGiver prizeGiver, ISaveSystem saveSystem)
        {
            _playerHealthSystem = playerHealth;
            _enemyHealthSystem = enemyHealth;
            _inventory = inventory;
            _prizeGiver = prizeGiver;
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            HealthSystem.onDieEvent += RestartGame;
        }
        private void OnDestroy()
        {
            HealthSystem.onDieEvent -= RestartGame;
        }

        public void RestartGame()
        {
            if (_playerHealthSystem.health > 0 && _enemyHealthSystem.health > 0) return;

            bool isWin = _playerHealthSystem.health > 0;

            _playerHealthSystem.ResetHealth();
            _enemyHealthSystem.ResetHealth();

            if (isWin)
            {
                _prizeGiver.GivePrize();
            }
            else
            {
                _inventory.ResetToDefault();
                _playerHealthSystem.ResetArmor();
            }

            _saveSystem.Save();
        }
    }
}
