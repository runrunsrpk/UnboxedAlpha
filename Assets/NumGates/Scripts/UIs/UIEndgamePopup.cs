using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates
{
    public class UIEndgamePopup : MonoBehaviour
    {
        [SerializeField] private UIGameplayText uiScore;
        [SerializeField] private UIGameplayText uiCrypto;
        //[SerializeField] private UIGameplayText uiDiamond;

        [SerializeField] private Transform highscore;

        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;

        private GameplayManager gameplayManager;

        public void Show()
        {
            gameObject.SetActive(true);

            InitUI();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdatePopup(int score, int crypto)
        {
            uiScore.UpdateText(score);
            uiCrypto.UpdateText(crypto);
        }

        public void ShowHighScore()
        {
            highscore.gameObject.SetActive(true);
        }

        //public void UpdatePopup(int score, int crypto, int diamond)
        //{
        //    uiScore.UpdateText(score);
        //    uiCrypto.UpdateText(crypto);
        //    uiDiamond.UpdateText(diamond);
        //}

        private void OnEnable()
        {
            retryButton.onClick.AddListener(OnClickRetry);
            exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(OnClickRetry);
            exitButton.onClick.RemoveListener(OnClickExit);
        }

        private void InitUI()
        {
            gameplayManager = GameManager.Instance.GameplayManager;

            uiScore.InitUI();
            uiCrypto.InitUI();

            highscore.gameObject.SetActive(false);
        }

        #region Button
        private void OnClickRetry()
        {
            gameplayManager.OnRestartGame?.Invoke();
        }

        private void OnClickExit()
        {
            gameplayManager.OnExitGame?.Invoke();
        }
        #endregion
    }
}

