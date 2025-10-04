using DG.Tweening;
using UnityEngine.EventSystems;

namespace Abstraction
{
    public class ButtonClosePanel : UiBase
    {
        public override void PointerClick()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        public override void PointerEnter()
        {
            Rt.DOKill();
            Rt.DOScale(1.05f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        public override void PointerExit()
        {
            Rt.DOKill();
            Rt.DOScale(1f, 0.25f);
        }

    }
}