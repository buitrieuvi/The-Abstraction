using DG.Tweening;
using UnityEngine;

namespace Abstraction
{
    public class HoverBase : UiBase
    {
        public override void PointerClick()
        {

        }

        public override void PointerEnter()
        {
            Rt.DOScale(1.1f, 0.5f);
        }

        public override void PointerExit()
        {
            Rt?.DOKill();
            Rt.DOScale(1f, 0.5f);
        }
    } 
}
