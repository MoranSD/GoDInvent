using Gameplay.InventorySystem.Items;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using Zenject;
using TMPro;

namespace Gameplay.FightSystem.Health.UI
{
    public class ArmorIconsDisplay : MonoBehaviour
    {
        [SerializeField] private ArmorIcon[] _icons;

        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void Start()
        {
            HealthSystem.onChangedEvent += UpdateStats;
            UpdateStats();
        }
        private void OnDestroy()
        {
            HealthSystem.onChangedEvent -= UpdateStats;
        }

        private void UpdateStats()
        {
            Clear();

            var armors = _playerHealth.equippedArmors;
            for (int i = 0; i < armors.Count; i++)
                SetVisual(armors[i]);
        }
        private void SetVisual(IArmor armor)
        {
            var armorIcon = _icons.First(x => x.type == armor.armorType);
            armorIcon.image.color = Color.white;
            armorIcon.image.sprite = armor.icon;
            armorIcon.count.text = armor.armorPoints.ToString();
        }
        private void Clear()
        {
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].image.color = new Color(0, 0, 0, 0);
                _icons[i].count.text = "0";
            }
        }

        [System.Serializable]
        private struct ArmorIcon
        {
            public Image image;
            public ArmorType type;
            public TextMeshProUGUI count;
        }
    }
}
