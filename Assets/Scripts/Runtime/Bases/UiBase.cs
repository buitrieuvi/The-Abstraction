using DG.Tweening;
using System;
using System.Collections.Generic;
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
        protected Canvas Canvas { get; set; }

        public virtual void Awake()
        {
            Button = GetComponent<Button>();
            Rt = GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();
            Canvas = GetComponent<Canvas>();

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
    } 
}
