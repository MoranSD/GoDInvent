using Gameplay.FightSystem.Health;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class EndGamePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;

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
        private void ShowPopup() => _popup.SetActive(true);
    }
}
