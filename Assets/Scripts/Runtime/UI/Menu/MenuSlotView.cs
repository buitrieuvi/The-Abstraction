using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Abstraction 
{
    public class MenuSlotView : SlotViewBase
    {
        public MenuSlot Slot => base.SlotBase as MenuSlot;

        public Image Bg;

        public bool NoScale;

        public override void Awake()
        {
            base.Awake();
            Bg = GetComponent<Image>();
        }

        public void Hover()
        {
            if (NoScale) return;

            Rt?.DOKill();
            Rt.DOScale(1.1f, 0.35f);
        }

        public void UnHover()
        {
            Rt?.DOKill();
            Rt.DOScale(1f, 0.35f);
        }

        public override void OnSelected(bool slot)
        {
            if (slot)
            {
                Bg.color = Color.black;
                TextShow.color = Color.white;
                NoScale = true;

                Hover();
            }
            else
            {
                Bg.color = Color.white;
                TextShow.color = Color.black;
                NoScale = false;
            }
        }
    }
}
