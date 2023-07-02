using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates
{
    public class UIGameplay : MonoBehaviour
    {
        [Header("UI Outer Frames")]
        [SerializeField] private Transform leftFrame;
        public Transform LeftFrame => leftFrame;

        [SerializeField] private Transform rightFrame;
        public Transform RightFrame => rightFrame;

        [SerializeField] private Transform topFrame;
        public Transform TopFrame => topFrame;

        [SerializeField] private Transform bottomFrame;
        public Transform BottomFrame => bottomFrame;

        [Header("UI Inner Frames")]
        [SerializeField] private Transform contentTopFrame;
        public Transform ContentTopFrame => contentTopFrame;

        [SerializeField] private Transform contentBottomFrame;
        public Transform ContentBottomFrame => contentBottomFrame;

        [Header("UI Components")]
        [SerializeField] private Transform uiScore;
        [SerializeField] private Transform uiUnboxedSymbols;
        [SerializeField] private Transform uiUnboxedGauge;
        [SerializeField] private Transform uiDiamond;
        [SerializeField] private Transform uiCrypto;
        [SerializeField] private Transform uiHealth;

        private TextMeshProUGUI scoreText;
        private TextMeshProUGUI diamondText;
        private TextMeshProUGUI cryptoText;

        [Header("UI Button")]
        [SerializeField] private Button pauseButton;

        private GameManager gameManager;
        private GameplayManager gameplayManager;

        private void Start()
        {
            InitManager();
            InitUI();
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;
            gameplayManager = gameManager.GameplayManager;

            gameplayManager.OnSoulCollected += SoulCollected;
        }

        private void InitUI()
        {
#if UNITY_STANDALONE

            diamondText = uiDiamond.GetComponentInChildren<TextMeshProUGUI>();
            diamondText.text = "0";
            uiDiamond.gameObject.SetActive(false);
#endif
            uiUnboxedGauge.gameObject.SetActive(false);

            cryptoText = uiCrypto.GetComponentInChildren<TextMeshProUGUI>();
            cryptoText.text = "0";

            scoreText = uiScore.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            scoreText.text = "0";
        }

        private void SoulCollected(int value)
        {
            int currentValue = int.Parse(scoreText.text);

            scoreText.text = (currentValue + value).ToString();
        }
    }
}

