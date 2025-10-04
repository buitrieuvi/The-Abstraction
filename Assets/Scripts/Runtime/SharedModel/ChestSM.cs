using System;
using System.Collections.Generic;

namespace Abstraction.SharedModel
{
    [Serializable]
    public class ChestSM : InventorySM
    {
        public string Id;

        public ChestSM(string id, List<InventorySlotSM> items) : base(items)
        {
            Id = id;
        }
    }
}