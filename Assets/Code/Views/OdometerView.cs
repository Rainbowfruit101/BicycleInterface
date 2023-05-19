using BicycleInterface.Code.Views.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BicycleInterface.Code.Views
{
    public class OdometerView: View
    {
        [SerializeField] private TMP_InputField testInput;
        [SerializeField] private TMP_Text valuePlace;
        [SerializeField] private float valueAnimationDuration;

        private float _currentValue;
        
        public override RectTransform SelfTransform => transform as RectTransform;
        public override bool IsShown { get; protected set; }
        public override bool IsInteractable { get; protected set; }

        private void Awake()
        {
            IsInteractable = true;
            testInput.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string currentValue)
        {
            if(!float.TryParse(currentValue, out var value))
                return;
            
            SetValue(value);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            IsShown = true;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            IsShown = false;
        }

        public void SetValue(float value)
        {
            IsInteractable = false;
            
            value = Mathf.Abs(value);
            DOTween.To(ValueGetter, ValueSetter, value, valueAnimationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => IsInteractable = true);
        }
        
        private float ValueGetter()
        {
            return _currentValue;
        }
        
        private void ValueSetter(float value)
        {
            _currentValue = value;
            var textValue = _currentValue
                .ToString("0000.000")
                .Replace(",", "\n,");

            textValue = LowColor8(textValue);
            valuePlace.text = textValue;
        }

        private string LowColor8(string value)
        {
            var start8 = string.Empty;
            
            while (value[0] == '0')
            {
                start8 += '8';
                value = value.Substring(1);
            }

            var end8 = string.Empty;
            
            while (value[value.Length - 1] == '0')
            {
                end8 += '8';
                value = value.Substring(0, value.Length - 1);
            }

            var color = ColorUtility.ToHtmlStringRGB(MakeLowColor(valuePlace.color)); 
            return $"<color #{color}>{start8}</color>{value}<color #{color}>{end8}</color>";
        }

        private Color MakeLowColor(Color color)
        {
            Color.RGBToHSV(color, out var h, out var s, out var v);
            v *= 0.2f;

            return Color.HSVToRGB(h, s, v);
        }
    }
}