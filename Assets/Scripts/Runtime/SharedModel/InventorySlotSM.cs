
namespace Abstraction.SharedModel 
{
    public class InventorySlotSM
    {
        public string Id;
        public int Quantity;

        public InventorySlotSM() { }

        public InventorySlotSM(string id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }

}