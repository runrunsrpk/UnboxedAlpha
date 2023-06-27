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

        private GameplayManager gameplayManager;

        private void InitUI()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
            
            UpdateText(3);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            InitUI();

            gameplayManager.OnUpdateCountdownTimer += UpdateText;
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            gameplayManager.OnUpdateCountdownTimer -= UpdateText;
        }

        private void UpdateText(float timer)
        {
            countdownText.text = timer.ToString();
        }
    }
}

