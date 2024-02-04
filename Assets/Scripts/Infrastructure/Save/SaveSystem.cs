using Gameplay.InventorySystem;
using Gameplay.FightSystem.Health;
using Gameplay.FightSystem;
using Zenject;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Infrastructure.Save
{
    public class SaveSystem : IInitializable, ISaveSystem
    {
        private Dictionary<string, object> _data = new();

        private Inventory _inventory;
        private PlayerHealth _playerHealth;
        private EnemyHealth _enemyHealth;
        private PlayerAttackSystem _playerAttack;
        public SaveSystem(Inventory inventory, PlayerHealth playerHealth, EnemyHealth enemyHealth, PlayerAttackSystem playerAttack)
        {
            _inventory = inventory;
            _playerHealth = playerHealth;
            _enemyHealth = enemyHealth;
            _playerAttack = playerAttack;
        }
        public void Initialize()
        {
            Load();
        }

        public void Load()
        {
            try
            {
                var b = new BinaryFormatter();
                using (FileStream stream = new FileStream(Application.persistentDataPath + "/SaveData.xml", FileMode.Open))
                {
                    _data = (Dictionary<string, object>)b.Deserialize(stream);
                }
            }
            catch
            {
                Debug.Log("Failed to load data");
            }


            if (_data.ContainsKey("Inventory")) _inventory.SetData(_data["Inventory"]);
            else _inventory.ResetToDefault();

            if (_data.ContainsKey("PlayerHealth")) _playerHealth.SetData(_data["PlayerHealth"]);
            if (_data.ContainsKey("EnemyHealth")) _enemyHealth.SetData(_data["EnemyHealth"]);
        }

        public void Save()
        {
            _data.Clear();

            _data.Add("Inventory", _inventory.GetData());
            _data.Add("PlayerHealth", _playerHealth.GetData());
            _data.Add("EnemyHealth", _enemyHealth.GetData());

            var b = new BinaryFormatter();
            using (FileStream stream = new FileStream(Application.persistentDataPath + "/SaveData.xml", FileMode.Create))
            {
                b.Serialize(stream, _data);
            }

            Debug.Log(Application.persistentDataPath + "/SaveData.xml");
        }
    }
}
