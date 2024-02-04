using UnityEngine;
using Zenject;

namespace Gameplay.FightSystem.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private Health.UI.ArmorIconsDisplay _armorDisplay;
        [SerializeField] private Transform _playerBar;
        [SerializeField] private Transform _enemyBar;

        private Health.PlayerHealth _playerHealth;
        private Health.EnemyHealth _enemyHealth;

        [Inject]
        private void Construct(Health.PlayerHealth playerHealth, Health.EnemyHealth enemyHealth)
        {
            _playerHealth = playerHealth;
            _enemyHealth = enemyHealth;
        }

        private void Start()
        {
            Health.HealthSystem.onChangedEvent += UpdateStats;
            UpdateStats();
        }
        private void OnDestroy()
        {
            Health.HealthSystem.onChangedEvent -= UpdateStats;
        }

        private void UpdateStats()
        {
            float playerHealth = (float)_playerHealth.health / (float)_playerHealth.maxHealth;
            float enemyHealth = (float)_enemyHealth.health / (float)_enemyHealth.maxHealth;

            playerHealth = Mathf.Clamp(playerHealth, 0, 1);
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 1);

            _playerBar.localScale = Vector3.right * playerHealth + Vector3.up + Vector3.forward;
            _enemyBar.localScale = Vector3.right * enemyHealth + Vector3.up + Vector3.forward;
        }
    }
}
