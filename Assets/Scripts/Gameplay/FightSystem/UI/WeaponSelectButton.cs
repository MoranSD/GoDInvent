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
        private void Awake()
        {
            _attackSystem.onChangeWeaponEvent += OnWeaponChanged;
            OnWeaponChanged();
        }
        private void OnDestroy()
        {
            _attackSystem.onChangeWeaponEvent -= OnWeaponChanged;
        }
        public void SetWeapon()
        {
            _attackSystem.SetWeapon(_weaponType);
        }

        private void SetVisualSelected(bool state) => _selected.SetActive(state);
        private void OnWeaponChanged()
        {
            SetVisualSelected(_attackSystem.currentWeapon == _weaponType);
        }
    }
}
