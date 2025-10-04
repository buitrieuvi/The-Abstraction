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


        public void SetView(Sprite img, int quantity, Color rank)
        {
            _img.sprite = img;
            _quantitty.text = quantity.ToString();
            _rank.color = rank;

            _img.gameObject.SetActive(true);
            _quantitty.transform.parent.gameObject.SetActive(true);

            IsData = true;
        }

        public void SetViewNull()
        {
            _img.gameObject.SetActive(false);
            _quantitty.transform.parent.gameObject.SetActive(false);

            IsData = false;
        }
    }
}

