using UnityEngine;
using UnityEngine.Events;
namespace Abstraction 
{
    public class SlotBase : MonoBehaviour
    {
        public SlotViewBase SlotViewBase { get; set; }

        public void Awake()
        {
            SlotViewBase = GetComponentInChildren<SlotViewBase>(true);
        }

        public virtual void OnSelected(bool slot)
        {
            SlotViewBase.OnSelected(slot);
        }

    }

}
