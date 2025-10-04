using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Abstraction
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownBase : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        public TMP_Dropdown Dropdown => _dropdown;

        public Dictionary<string, Action> Dic = new();

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        public void Start()
        {
            //foreach (var option in _dropdown.options) 
            //{
            //    SlotViewBase slotView = option;
            //}
        }

        private void OnValueChanged(int index)
        {
            var keys = Dic.Keys.ToList();
            if (index >= 0 && index < keys.Count)
            {
                string key = keys[index];
                Dic[key]?.Invoke();
            }
        }

        public void AddDropdown()
        {
            _dropdown.AddOptions(Dic.Keys.ToList());

            int itemCount = _dropdown.options.Count;
            float itemHeight = 35 + 10f;
            float totalHeight = itemCount * itemHeight + 10;

            RectTransform template = _dropdown.template;
            template.sizeDelta = new Vector2(template.sizeDelta.x, totalHeight);
        }
    }

}