using Abstraction.SharedModel;
using System.Data;
using UnityEngine;

namespace Abstraction
{
    public class InventorySlot : SlotBase
    {
        public InventorySlotView View => SlotViewBase as InventorySlotView;

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
