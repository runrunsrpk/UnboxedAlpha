using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIHome : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button storeButton;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button optionsButton;

        private GameplayManager gameplayManager;
        private UIManager uiManager;

        public void Show()
        {
            gameObject.SetActive(true);

            InitUI();
        }

        public void Hide()
        {
            gameObject.SetActive(false);

        }

        private void OnEnable()
        {
            startButton.onClick.AddListener(OnClickStart);
            upgradeButton.onClick.AddListener(OnClickUpgrade);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnClickStart);
            upgradeButton.onClick.RemoveListener(OnClickUpgrade);
        }

        private void InitUI()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
            uiManager = GameManager.Instance.UIManager;
        }

        #region Button
        private void OnClickStart()
        {
            gameplayManager.OnStartGame?.Invoke();
        }

        private void OnClickStore()
        {

        }

        private void OnClickUpgrade()
        {
            uiManager.UIUpgrade.Show();
        }

        private void OnClickOptions()
        {

        }
        #endregion
    }
}

