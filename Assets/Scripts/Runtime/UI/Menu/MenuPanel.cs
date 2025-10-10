using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Abstraction 
{
    public class MenuPanel : PanelBase
    {
        [SerializeField] private ScrollRect menu_C;
        [SerializeField] private GameObject menu_L;

        private List<MenuSlot> menuSlots_L;
        private List<MenuSlot> menuSlots_C;

        [SerializeField] DropdownBase _dropdownFPS;

        [SerializeField] SliderBase _sliderCameraX;
        [SerializeField] SliderBase _sliderCameraY;

        [Inject] CameraManager _cameraManager;
        public override void Awake()
        {
            base.Awake();

            menuSlots_L = menu_L.transform.GetComponentsInChildren<MenuSlot>().ToList();
            menuSlots_C = menu_C.content.GetComponentsInChildren<MenuSlot>().ToList();

            _dropdownFPS.Dic.Add("Không giới hạn", () => Application.targetFrameRate =-1);
            _dropdownFPS.Dic.Add("300 FPS", () => Application.targetFrameRate = 300);
            _dropdownFPS.Dic.Add("200 FPS", () => Application.targetFrameRate = 200);
            _dropdownFPS.Dic.Add("100 FPS", () => Application.targetFrameRate = 100);
            _dropdownFPS.Dic.Add("60 FPS", () => Application.targetFrameRate = 60);
            _dropdownFPS.Dic.Add("30 FPS", () => Application.targetFrameRate = 30);

            _dropdownFPS.AddDropdown();
        }

        public override void Start()
        {
            base.Start();

            menuSlots_L.ForEach(slot =>
            {
                slot.View.ClickEvt += evt =>
                {
                    if (slot == _selectedSlot) { return; }

                    menuSlots_C.ForEach(a => a.gameObject.SetActive(false));
                    var mainList = menuSlots_C.Where(b => b.tag == slot.tag).ToList();
                    mainList.ForEach(a => a.gameObject.SetActive(true));

                    OnSelect(slot);
                };

                slot.View.HoverEvt += evt =>
                {
                    
                };

                slot.View.UnHoverEvt += evt =>
                {
                    
                };
            });

            menuSlots_C.ForEach(x => { x.gameObject.SetActive(false);});
            menuSlots_L[0].View.PointerClick();

            // Set

            _sliderCameraX.SetValue(20);
            _sliderCameraY.SetValue(20);

            CameraMg.SetOrbitX(_sliderCameraX.GetValue);
            CameraMg.SetOrbitY(_sliderCameraY.GetValue);

            CameraMg.SetPan(_sliderCameraX.GetValue);
            CameraMg.SetTilt(_sliderCameraY.GetValue);

            _sliderCameraX.OnSliderValueChanged += () =>
            {
                CameraMg.SetOrbitX(_sliderCameraX.GetValue);
                CameraMg.SetPan(_sliderCameraX.GetValue);
            };

            _sliderCameraY.OnSliderValueChanged += () =>
            {
                CameraMg.SetOrbitY(_sliderCameraY.GetValue);
                CameraMg.SetTilt(_sliderCameraY.GetValue);
            };
        }

        public override void Open()
        {
            base.Open();

            menuSlots_L.ForEach(x => {
                var pos = x.View.Rt.anchoredPosition;
                pos.x = -500f;
                x.View.Rt.anchoredPosition = pos;
            });
        }

        public override void Close()
        {
            base.Close();
        }

        public override void CloseImmediately()
        {
            base.CloseImmediately();
        }

        public override void OnCloseButton()
        {
            base.OnCloseButton();
        }

        public override void OnCompleted()
        {
            Sequence sequence = DOTween.Sequence();

            foreach (var slot in menuSlots_L)
            {
                sequence.Join(slot.View.Rt.DOAnchorPosX(0f, 0.25f)
                    .From(new Vector2(-500, slot.View.Rt.anchoredPosition.y))
                    .SetDelay(slot.transform.GetSiblingIndex() * 0.035f));
            }

            sequence.OnComplete(() => base.OnCompleted());
            sequence.Play();
        }

        public override void OnCloseCompleted()
        {
            base.OnCloseCompleted();
        }
    }
}