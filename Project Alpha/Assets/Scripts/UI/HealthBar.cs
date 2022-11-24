using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
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

        public void SetHealth(float health)
        {
            _slider.value = health;
        }

        public void SetMaxHealth(float maxHealth)
        {
            _slider.maxValue = maxHealth;
            float newWidth = Math.Min(_baseWidth + maxHealth, maxWidth);
        
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        }
    }
}
