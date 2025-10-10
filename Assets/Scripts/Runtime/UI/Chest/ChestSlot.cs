namespace Abstraction
{
    public class ChestSlot : SlotBase
    {
        public ChestSlotView View => SlotViewBase as ChestSlotView;

        public ItemSO Item { get; set; }
        public int Quantity { get; set; }

        public override void SetItem()
        {
            base.SetItem();
        }

        public override void OnSelect(bool isSelect)
        {
            base.OnSelect(isSelect);
        }
    }
}

