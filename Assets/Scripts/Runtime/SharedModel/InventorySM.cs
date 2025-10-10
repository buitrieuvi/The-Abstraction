using System.Collections.Generic;
using System;

namespace Abstraction.SharedModel
{
	[Serializable]
	public class InventorySM
	{
		public List<InventorySlotSM> Items = new();

        public InventorySM()
        {
        }

        public InventorySM(List<InventorySlotSM> items)
        {
            Items = items;
        }
    }
}