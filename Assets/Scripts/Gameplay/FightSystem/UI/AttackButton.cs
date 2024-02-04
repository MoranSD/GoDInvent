using UnityEngine;
using Zenject;

namespace Gameplay.FightSystem.UI
{
    public class AttackButton : MonoBehaviour
    {
        [SerializeField] private Gameplay.UI.Popups.ItemPopupMenuService _itemPopup;
        private PlayerAttackSystem _attackSystem;

        [Inject]
        private void Construct(PlayerAttackSystem attackSystem)
        {
            _attackSystem = attackSystem;
        }
        public void Attack()
        {
            if (_itemPopup.isMenuOpened) return;

            _attackSystem.Attack();
        }
    }
}
