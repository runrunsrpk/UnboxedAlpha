using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIPausePopup : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;

        private GameplayManager gameplayManager;

        public void Show()
        {
            gameObject.SetActive(true);

            InitUI();

            resumeButton.onClick.AddListener(OnClickResume);
            retryButton.onClick.AddListener(OnClickRetry);
            exitButton.onClick.AddListener(OnClickExit);
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            resumeButton.onClick.RemoveListener(OnClickResume);
            retryButton.onClick.RemoveListener(OnClickRetry);
            exitButton.onClick.RemoveListener(OnClickExit);
        }

        private void InitUI()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
        }

        #region Button
        private void OnClickResume()
        {
            gameplayManager.OnStartCountdownTimer?.Invoke();
        }

        private void OnClickRetry()
        {

        }

        private void OnClickExit()
        {
            gameplayManager.OnExitGame?.Invoke();
        }
        #endregion
    }
}

