using UnityEngine;
using Zenject;

namespace Gameplay.FightSystem.UI
{
    public class AttackButton : MonoBehaviour
    {
        private PlayerAttackSystem _attackSystem;

        [Inject]
        private void Construct(PlayerAttackSystem attackSystem)
        {
            _attackSystem = attackSystem;
        }
        public void Attack() => _attackSystem.Attack();
    }
}
