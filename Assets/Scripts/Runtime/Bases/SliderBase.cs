using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Abstraction 
{
    [RequireComponent(typeof(Slider))]
    public class SliderBase : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;

        public UnityAction OnSliderValueChanged { get; set; }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _text.text = _slider.value.ToString();

            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float index)
        {
            _text.text = index.ToString();
            OnSliderValueChanged?.Invoke();
        }

        public float GetValue => _slider.value;
        public void SetValue(int f) => _slider.value = f;
    }
}
