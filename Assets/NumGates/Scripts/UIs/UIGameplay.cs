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
        [SerializeField] private UITimer uiTimer;
        [SerializeField] private UIUnboxed uiUnboxed;
        [SerializeField] private UIGameplayText uiScore;
        [SerializeField] private UIGameplayText uiDiamond;
        [SerializeField] private UIGameplayText uiCrypto;
        [SerializeField] private UIHealth uiHealth;

        [Header("UI Button")]
        [SerializeField] private Button pauseButton;

        private GameManager gameManager;
        private GameplayManager gameplayManager;

        #region Initialize
        public void Initialize()
        {
            InitManager();
            InitUI();

            EnableAction();
        }

        public void Terminate()
        {
            DisableAction();
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;
            gameplayManager = gameManager.GameplayManager;
        }

        private void InitUI()
        {
#if UNITY_STANDALONE

            uiDiamond.InitUI();
            uiDiamond.Hide();
#endif
            uiUnboxed.InitUI();
            uiHealth.InitUI();
            uiCrypto.InitUI();
            uiScore.InitUI();
            uiTimer.InitUI();
        }
        
        private void EnableAction()
        {
            gameplayManager.OnSoulCollected += SoulCollected;
            gameplayManager.OnCryptoCollected += CryptoCollected;
            gameplayManager.OnDiamondCollected += DiamondCollected;
            gameplayManager.OnSymbolCollected += SymbolCollected;
            gameplayManager.OnSoulMissed += SoulMissed;

            gameplayManager.OnUpdateGameTimer += UpdateGameTimer;

            gameplayManager.OnStartBonusTimer += StartBonusTimer;
            gameplayManager.OnUpdateBonusTimer += UpdateBonusTimer;
            gameplayManager.OnEndBonusTimer += EndBonusTimer;

            gameplayManager.OnStartShieldTimer += StartShieldTimer;
            gameplayManager.OnEndShieldTimer += EndShieldTimer;
        }

        private void DisableAction()
        {

        }
        #endregion

        #region Action Collect
        private void SoulCollected(int value)
        {
            uiScore.UpdateTextValue(value);
        }

        private void CryptoCollected(int value)
        {
            uiCrypto.UpdateTextValue(value);
        }

        private void DiamondCollected(int value)
        {
            uiDiamond.UpdateTextValue(value);
        }

        private void SymbolCollected()
        {
            uiUnboxed.AddSymbol();
        }

        private void SoulMissed()
        {
            if(gameplayManager.IsShield == false)
            {
                uiHealth.Damaged();
            }
        }
        #endregion

        #region Action Timer
        // Timer
        private void UpdateGameTimer(float timer, float maxTimer)
        {
            uiTimer.UpdateTimerText(timer);
            uiTimer.UpdateTimer(timer, maxTimer);
        }

        private void EndGameTimer()
        {
            //TODO: close bg
        }

        // Bonus
        private void StartBonusTimer()
        {
            uiUnboxed.ActivateBonus();
        }

        private void UpdateBonusTimer(float timer, float maxTimer)
        {
            uiUnboxed.UpdateBonus(timer, maxTimer);
        }

        private void EndBonusTimer()
        {
            uiUnboxed.DeactivateBonus();
        }

        // Shield
        private void StartShieldTimer()
        {
            uiHealth.EnableShield();
        }

        private void EndShieldTimer()
        {
            uiHealth.DisableShield();
        }
        #endregion
    }
}

