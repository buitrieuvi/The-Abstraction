using System.Collections.Generic;
using System.Linq;
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

            _dropdownFPS.Dic.Add("Không giới hạn", () => SetFPS(-1));
            _dropdownFPS.Dic.Add("300 FPS", () => SetFPS(300));
            _dropdownFPS.Dic.Add("200 FPS", () => SetFPS(200));
            _dropdownFPS.Dic.Add("100 FPS", () => SetFPS(100));
            _dropdownFPS.Dic.Add("60 FPS", () => SetFPS(60));
            _dropdownFPS.Dic.Add("30 FPS", () => SetFPS(30));

            _dropdownFPS.AddDropdown();


        }

        public override void Start()
        {
            base.Start();

            menuSlots_L.ForEach(slot =>
            {
                slot.SlotViewBase.ClickEvt += evt =>
                {
                    if (slot == SelectSlot) { return; }

                    menuSlots_C.ForEach(a => a.gameObject.SetActive(false));
                    var mainList = menuSlots_C.Where(b => b.tag == slot.tag).ToList();
                    mainList.ForEach(a => a.gameObject.SetActive(true));

                    DoTweenExtensions.DoSequenceScaleXDelay(mainList, null, 0.15f, 0.00025f);
                };

                slot.SlotViewBase.HoverEvt += evt =>
                {
                    slot.View.Hover();
                };

                slot.SlotViewBase.UnHoverEvt += evt =>
                {
                    slot.View.UnHover();
                };

                slot.SlotViewBase.ClickEvt += evt =>
                {
                    OnSelected(slot);
                };
            });

            menuSlots_C.ForEach(x => { x.gameObject.SetActive(false); });
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
            DoTweenExtensions.DoSequenceMoveDelay(menuSlots_L, base.OnCompleted, 0.25f, 0.035f);
        }

        public override void OnCloseCompleted()
        {
            base.OnCloseCompleted();
        }

        private void SetFPS(int fps)
        {
            Application.targetFrameRate = fps;
        }
    }
}