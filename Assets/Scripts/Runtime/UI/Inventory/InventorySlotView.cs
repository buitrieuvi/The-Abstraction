using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Abstraction 
{
    public class InventorySlotView : SlotViewBase
    {
        public InventorySlot InventorySlot => SlotBase as InventorySlot;

        [SerializeField] GameObject _dataView;

        [SerializeField] Image _mainImg;
        [SerializeField] Image _rankColor;
        [SerializeField] TMP_Text _quantity;
        [SerializeField] Image _select;

        public override void Awake()
        {
            base.Awake();
        }

        public void Start()
        {

        }

        public override void SetItem()
        {
            if (InventorySlot.Item == null || InventorySlot.Quantity == 0)
            {
                _dataView.gameObject.SetActive(false);

                _quantity.text = "Trống";
                _select.gameObject.SetActive(false);
                CanvasGroup.alpha = 0.95f;
            }
            else 
            {
                _dataView.gameObject.SetActive(true);

                _mainImg.sprite = InventorySlot.Item.Sprite;
                _rankColor.color = InventorySlot.Item.Rank.Color;

                _quantity.text = InventorySlot.Quantity.ToString();
                CanvasGroup.alpha = 1f;
            }
        }

        public override void OnSelect(bool isSelect)
        {
            _select.gameObject.SetActive(isSelect);
        }
    }
}
