using System.Collections.Generic;

namespace Abstraction.SharedModel
{
    public class PlayerSM
    {
        public string Id = "";
        public string Name = "";

        public InventorySM Inventory = new();
        public List<ChestSM> Chests = new();

        public PlayerSM()
        {
            
        } 
    }
}
