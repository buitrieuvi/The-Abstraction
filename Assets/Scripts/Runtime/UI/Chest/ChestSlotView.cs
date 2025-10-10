using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
namespace Abstraction
{
    public class ChestSlotView : SlotViewBase
    {
        public ChestSlot ChestSlot => SlotBase as ChestSlot;

        [SerializeField] Image _img;
        [SerializeField] TMP_Text _quantitty;
        [SerializeField] Image _rank;

        public Image Img => _img;
        public TMP_Text Quantitty => _quantitty;
        public Image Rank => _rank;

        public override void SetItem()
        {
            base.SetItem();

            if (ChestSlot.Item != null || ChestSlot.Quantity > 0) 
            {
                _img.sprite = ChestSlot.Item.Sprite;
                _quantitty.text = ChestSlot.Quantity.ToString();
                _rank.color = ChestSlot.Item.Rank.Color;

                _img.gameObject.SetActive(true);
                _quantitty.transform.parent.gameObject.SetActive(true);
            } 
            else
            {
                _img.gameObject.SetActive(false);
                _quantitty.transform.parent.gameObject.SetActive(false);
            }

        }

        public override void OnSelect(bool isSelect)
        {
            base.OnSelect(isSelect);
        }


        //public void SetView(ItemSO item, int quantity)
        //{
        //    _img.sprite = item.Sprite;
        //    _quantitty.text = quantity.ToString();
        //    _rank.color = item.Rank.Color;

        //    _img.gameObject.SetActive(true);
        //    _quantitty.transform.parent.gameObject.SetActive(true);
        //}

        //public void SetViewNull()
        //{
        //    _img.gameObject.SetActive(false);
        //    _quantitty.transform.parent.gameObject.SetActive(false);
        //}
    }
}

