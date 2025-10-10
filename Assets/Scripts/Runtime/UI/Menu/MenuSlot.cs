using UnityEditor.Graphs;
using UnityEngine.Events;

namespace Abstraction 
{
    public class MenuSlot : SlotBase
    {
        public MenuSlotView View => SlotViewBase as MenuSlotView;

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