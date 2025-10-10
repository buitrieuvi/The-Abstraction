using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Abstraction
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PanelBase : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private float _openAnimationDuration = 0.4f;
        private float _closeAnimationDuration = 0.3f;

        [Inject] protected InputController InputCtrl;
        [Inject] protected PanelManager PanelMg;
        [Inject] protected CameraManager CameraMg;
        [Inject] protected PlayerController PlayerCtrl;

        public bool IsPopup;

        [SerializeField] private ButtonClosePanel _btnClose;

        protected SlotBase _selectedSlot;

        public virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Start()
        {

        }

        public virtual void Open()
        {
            PanelMg.IsLoading = true;
            PanelMg.PanelSelected = this;
            PlayerCtrl.IsFreezing(true);
            CameraMg.SetLook(false);
            gameObject.SetActive(true);

            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(1f, _openAnimationDuration)
                .OnComplete(OnCompleted);

            _btnClose?.Button.onClick.AddListener(() =>
            {
                PanelMg.OnInputAction(this);
            });
        }

        public virtual void Close()
        {
            InputCtrl.IsCurrsor(false);
            CameraMg.SetLook(true);
            PanelMg.PanelSelected = null;
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0f, _closeAnimationDuration)
                .OnComplete(OnCloseCompleted);

            _btnClose?.Button.onClick.RemoveAllListeners();
        }

        public virtual void OnCompleted()
        {
            InputCtrl.IsCurrsor(true);

            PanelMg.IsLoading = false;
            _canvasGroup.interactable = true;

        }

        public virtual void OnCloseCompleted()
        {
            gameObject.SetActive(false);
            PanelMg.IsLoading = false;

            PlayerCtrl.IsFreezing(false);
        }

        public virtual void CloseImmediately() => OnCloseCompleted();

        public virtual void OnCloseButton() => Close();

        public virtual void OnSelect(SlotBase slot)
        {
            if (_selectedSlot != null)
            {
                _selectedSlot.OnSelect(false);
            }
            _selectedSlot = slot;
            _selectedSlot.OnSelect(true);
        }
    } 
}
