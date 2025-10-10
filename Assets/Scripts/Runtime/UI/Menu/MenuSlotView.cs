using DG.Tweening;
using TMPro;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;

namespace Abstraction 
{
    public class MenuSlotView : SlotViewBase
    {
        public MenuSlot Slot => SlotBase as MenuSlot;

        [SerializeField] Image _bg;
        [SerializeField] TMP_Text _text;

        public override void Awake()
        {   
            base.Awake();
        }

        public override void SetItem()
        {
            base.SetItem();
        }

        public override void OnSelect(bool isSelect)
        {
            base.OnSelect(isSelect);
            if (isSelect)
            {
                _bg.color = Color.black;
                _text.color = Color.white;
            }
            else
            {
                _bg.color = Color.white;
                _text.color = Color.black;
            }
        }
    }
}
