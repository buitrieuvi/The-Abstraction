using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Abstraction
{
    public class SlotViewBase : UiBase
    {
        public SlotBase SlotBase { get; set; }

        public event UnityAction<SlotBase> ClickEvt;
        public event UnityAction<SlotBase> HoverEvt;
        public event UnityAction<SlotBase> UnHoverEvt;

        public TMP_Text TextShow;
        public bool IsData;

        public override void Awake()
        {
            base.Awake();
            SlotBase = GetComponentInParent<SlotBase>(true);
        }

        public override void PointerClick()
        {
            ClickEvt?.Invoke(SlotBase);
        }

        public override void PointerEnter()
        {
            HoverEvt?.Invoke(SlotBase);
        }

        public override void PointerExit()
        {
            UnHoverEvt?.Invoke(SlotBase);
        }
    }
}