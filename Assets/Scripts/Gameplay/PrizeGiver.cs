using Gameplay.InventorySystem;

namespace Gameplay
{
    public class PrizeGiver
    {
        private Data.PrizeGiverConfig _config;
        private Inventory _inventory;
        public PrizeGiver(Data.PrizeGiverConfig config, Inventory inventory)
        {
            _config = config;
            _inventory = inventory;
        }

        public void GivePrize() 
        {
            var prize = _config.GetRandomPrize();
            _inventory.TryAddItem(prize.CreateItem());
        }
    }
}
