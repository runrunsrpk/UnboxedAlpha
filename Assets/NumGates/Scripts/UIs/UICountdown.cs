using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NumGates
{
    public class UICountdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateText(float timer)
        {
            countdownText.text = timer.ToString();
        }
    }
}

