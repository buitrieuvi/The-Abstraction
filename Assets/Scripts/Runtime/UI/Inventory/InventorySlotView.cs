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

        [SerializeField] GameObject _goUp;

        [SerializeField] Image _mainImg;
        [SerializeField] Image _rankColor;
        [SerializeField] TMP_Text _quantity;

        [SerializeField] Image _select;

        public GameObject GoUp => _goUp;

        public override void Awake()
        {
            base.Awake();
        }

        public void SetView(ItemSO item, RankSO rank, int quantity)
        {
            _goUp.gameObject.SetActive(true);

            _mainImg.sprite = item.Sprite;
            _rankColor.color = rank.Color;

            _quantity.text = quantity.ToString();
            CanvasGroup.alpha = 1f;

            IsData = true;
        }

        public void SetViewNull()
        {
            _goUp.gameObject.SetActive(false);

            _quantity.text = "Trống";
            _select.gameObject.SetActive(false);
            CanvasGroup.alpha = 0.95f;

            IsData = false;
        }

        public override void OnSelected(bool b)
        {
            _select.gameObject.SetActive(b);
        }
    }
}
