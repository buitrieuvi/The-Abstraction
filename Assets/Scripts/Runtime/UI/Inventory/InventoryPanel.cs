using Abstraction.SharedModel;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Abstraction
{
    public class InventoryPanel : PanelBase
    {
        [SerializeField] private ScrollRect _scroll;
        [SerializeField] private InventorySlot _prefabs;
        [SerializeField] private List<InventorySlot> _slots => _scroll.content.GetComponentsInChildren<InventorySlot>().ToList();

        [Inject] GameDataController _data;

        public override void Awake()
        {

            //_dataView.PlayerInventory.ObserveReplace()
            //.Subscribe(x =>
            //{
            //    OnResetInventory(_dataView.PlayerInventory);
            //}).AddTo(this);

            base.Awake();

            for (int i = 0; i < 8 * 5; i++)
            {
                InventorySlot slot = Instantiate(_prefabs, _scroll.content.transform);
                slot.SetItem();
                slot.View.ClickEvt += (slot =>
                {
                    OnSelect(slot);
                });
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Open()
        {
            OnResetInventory(_data.PlayerInventory);
            base.Open();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void OnCompleted()
        {
            Sequence sequence = DOTween.Sequence();

            foreach (var slot in _slots)
            {
                sequence.Join(slot.View.Rt.DOScale(1f, 0.35f)
                    .From(0f)
                    .SetDelay(slot.transform.GetSiblingIndex() * 0.000035f));
            }

            sequence.OnComplete(() => base.OnCompleted());
            sequence.Play();
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

        public void OnResetInventory(ReactiveCollection<InventorySlotSM> inventory)
        {
            int index = 0;

            foreach (var slot in inventory)
            {
                _slots[index].Item = _data.ItemSOs.Find(x => x.Id == slot.Id);
                _slots[index].Quantity = slot.Quantity;

                _slots[index].SetItem();

                index++;
            }

            while (true)
            {

                if (_slots[index].Item == null || _slots[index].Quantity == 0) break;

                _slots[index].Item = null;
                _slots[index].Quantity = 0;

                _slots[index].SetItem();

                index++;
            }
        }
    }
}