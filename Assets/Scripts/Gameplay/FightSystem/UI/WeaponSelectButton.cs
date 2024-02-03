using UnityEngine;
using Zenject;

namespace Gameplay.FightSystem.UI
{
    public class WeaponSelectButton : MonoBehaviour
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private GameObject _selected;

        private PlayerAttackSystem _attackSystem;

        [Inject]
        private void Construct(PlayerAttackSystem attackSystem)
        {
            _attackSystem = attackSystem;
        }

        public void SetWeapon()
        {
            _attackSystem.SetWeapon(_weaponType);
        }

        public void SetVisualSelected(bool state) => _selected.SetActive(state);
    }
}
