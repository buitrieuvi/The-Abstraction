using Abstraction.SharedModel;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace Abstraction
{
    public class InventoryPanel : PanelBase
    {

        [SerializeField] private ScrollRect _scroll;

        [SerializeField] private InventorySlot _prefabs;

        [SerializeField] private List<InventorySlot> _slots;

        [Inject] GameDataController _data;

        public override void Awake()
        {
            _data.PlayerInventory.ObserveReplace()
            .Subscribe(x =>
            {
                ResetInventory(_data.PlayerInventory);
            }).AddTo(this);

            base.Awake();
            _slots = new List<InventorySlot>();
            _slots = GetComponentsInChildren<InventorySlot>().ToList();
            _slots.ForEach(x => x.View.SetViewNull());

            for (int i = 0; i < 8 * 5; i++)
            {
                InventorySlot slot = Instantiate(_prefabs, _scroll.content.transform);
                slot.SlotViewBase.ClickEvt += data => base.OnSelected(slot);
                slot.View.SetViewNull();
                _slots.Add(slot);
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Open()
        {
            ResetInventory(_data.PlayerInventory);
            base.Open();
            _slots[0].View.PointerClick();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void OnCompleted()
        {
            DoTweenExtensions.TweenersAndAction(
                _slots.Select(
                    item => item.View.Ani_DOScale(1f, 0.25f, 0f, 0.00025f * item.transform.GetSiblingIndex())).ToList(),
                base.OnCompleted);
        }

        public override void CloseImmediately()
        {
            base.CloseImmediately();
        }

        public override void OnCloseButton()
        {
            base.OnCloseButton();
        }

        public override void OnCloseCompleted()
        {
            base.OnCloseCompleted();
        }

        public void ResetInventory(ReactiveCollection<InventorySlotSM> inventory)
        {
            _slots.Where(x => x.View.gameObject.activeSelf).ToList().ForEach(x => x.View.SetViewNull());
            int index = 0;

            foreach (var slot in inventory)
            {
                var a = _data.ItemSOs.First(x => x.Id == slot.Id);

                _slots[index].View.SetView(a, a.Rank, inventory[index].Quantity);
                index++;
            }

            while (_slots[index].View.GoUp.activeSelf)
            {
                _slots[index].View.SetViewNull();
                index++;
            }
        }
    }
}