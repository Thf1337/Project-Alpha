using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public float time = 0;

        private TextMeshProUGUI _timerText;

        private bool _stopTime;

        private void Start()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (!_stopTime)
            {
                time += Time.deltaTime;
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
