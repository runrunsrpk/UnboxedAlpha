using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIHome : MonoBehaviour
    {
        [Header("Text")]
        //[SerializeField] private UIGameplayText uiHighscore;
        [SerializeField] private UIGameplayText uiCrypto;
        //[SerializeField] private UIGameplayText uiDiamond;

        [Header("Button")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button storeButton;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button optionsButton;

        private GameManager gameManager;
        private GameplayManager gameplayManager;
        private UIManager uiManager;
        private PlayerManager playerManager;
        private AudioManager audioManager;

        public void Show()
        {
            gameObject.SetActive(true);

            InitUI();
        }

        public void Hide()
        {
            gameObject.SetActive(false);

        }

        public void UpdateUI()
        {
            uiCrypto.UpdateText(playerManager.GetCrypto());
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
            gameManager = GameManager.Instance;

            gameplayManager = gameManager.GameplayManager;
            uiManager = gameManager.UIManager;
            playerManager = gameManager.PlayerManager;
            audioManager = gameManager.AudioManager;

            //uiDiamond.UpdateText(playerManager.GetDiamond());
            //uiHighscore.UpdateText(playerManager.GetHighscore());
            uiCrypto.UpdateText(playerManager.GetCrypto());
        }

        #region Button
        private void OnClickStart()
        {
            gameplayManager.OnStartGame?.Invoke();

            audioManager.PlaySound(AudioSound.UIClick);
        }

        private void OnClickStore()
        {

        }

        private void OnClickUpgrade()
        {
            uiManager.UIUpgrade.Show();

            audioManager.PlaySound(AudioSound.UIClick);
        }

        private void OnClickOptions()
        {

        }
        #endregion
    }
}

