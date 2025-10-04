using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Abstraction
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UiBase : MonoBehaviour,
        IPointerClickHandler,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        public Button Button { get; set; }
        public RectTransform Rt { get; set; }
        protected CanvasGroup CanvasGroup { get; set; }


        //protected Canvas Canvas;

        public Tweener Ani_DOScale(float b, float t, float a, float delay) =>
            Rt.DOScale(b, t)
            .From(a).SetDelay(delay);

        public virtual void Awake()
        {
            Button = GetComponent<Button>();
            Rt = GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();
            //Canvas = GetComponent<Canvas>();

            if (Button != null)
            {
                Button.onClick.AddListener(PointerClick);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Button == null)
                PointerClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit();
        }

        public abstract void PointerClick();

        public abstract void PointerEnter();

        public abstract void PointerExit();

        public virtual void OnSelected(bool slot)
        {

        }

        public void Ani_AnchorPos_2D_Zero()
        {
            //Canvas.overrideSorting = true;
            //Canvas.sortingOrder = 1;

            Rt?.DOKill();
            Rt.DOAnchorPos(Vector2.zero, 3.25f);
        }

        public void AnchorPos_2D_Zero()
        {
            //Canvas.sortingOrder = 0;
            //Canvas.overrideSorting = false;
            Rt.anchoredPosition = Vector2.zero;
        }
    } 
}
