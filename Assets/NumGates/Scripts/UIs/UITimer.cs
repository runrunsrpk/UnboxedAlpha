using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Slider timerSlider;

        public void InitUI()
        {
            UpdateTimerText(60);
        }

        public void UpdateTimerText(float value)
        {
            timerText.text = ((int)value).ToString();
        }

        public void UpdateTimer(float value, float maxValue)
        {
            timerSlider.value = value / maxValue;
        }
    }
}

