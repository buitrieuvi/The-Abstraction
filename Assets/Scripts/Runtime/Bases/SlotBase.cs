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

        public virtual void SetItem()
        {
            SlotViewBase.SetItem();
        }

        public virtual void OnSelect(bool isSelect)
        {
            SlotViewBase.OnSelect(isSelect);
        }

    }

}
