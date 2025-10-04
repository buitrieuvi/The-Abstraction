using UnityEditor.Graphs;
using UnityEngine.Events;

namespace Abstraction 
{
    public class MenuSlot : SlotBase
    {
        public MenuSlotView View => SlotViewBase as MenuSlotView;
    }

}