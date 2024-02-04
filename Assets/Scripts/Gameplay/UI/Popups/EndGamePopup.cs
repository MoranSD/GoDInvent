using Gameplay.FightSystem.Health;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Popups
{
    public class EndGamePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;

        private PlayerHealth _playerHealth;
        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void Start()
        {
            _popup.SetActive(false);
            HealthSystem.onDieEvent += ShowPopup;
        }
        private void OnDestroy()
        {
            HealthSystem.onDieEvent -= ShowPopup;
        }
        public void RestartGame() => _popup.SetActive(false);
        private void ShowPopup()
        {
            if (_playerHealth.health > 0) return;

            _popup.SetActive(true);
        }
    }
}
