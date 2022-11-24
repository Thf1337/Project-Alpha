using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ManaBar : MonoBehaviour
    {
        [SerializeField] private float maxWidth = 1000f;
    
        private float _baseWidth;
    
        private Slider _slider;
        private RectTransform _rectTransform;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _rectTransform = GetComponent<RectTransform>();

            _baseWidth = _rectTransform.rect.width;
        }

        public void SetMana(float mana)
        {
            _slider.value = mana;
        }

        public void SetMaxMana(float maxMana)
        {
            _slider.maxValue = maxMana;
            float newWidth = Math.Min(_baseWidth + maxMana, maxWidth);
        
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        }
    }
}