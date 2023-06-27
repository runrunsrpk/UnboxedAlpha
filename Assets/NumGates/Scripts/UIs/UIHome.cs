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
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnClickStart);
        }

        private void InitUI()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
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

        }

        private void OnClickOptions()
        {

        }
        #endregion
    }
}

