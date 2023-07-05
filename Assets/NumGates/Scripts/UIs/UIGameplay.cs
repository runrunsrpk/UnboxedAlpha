using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates
{
    public class UIGameplay : MonoBehaviour
    {
        public int Score => uiScore.GetTextValue();
        public int Crypto => uiCrypto.GetTextValue();
        public int Diamond => uiDiamond.GetTextValue();

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
            pauseButton.onClick.AddListener(OnClickPause);
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnClickPause);
        }

        private void InitUI()
        {
            gameManager = GameManager.Instance;
            gameplayManager = gameManager.GameplayManager;

#if UNITY_STANDALONE
            uiDiamond.Hide();
#else
            uiDiamond.InitUI();
#endif
            uiUnboxed.InitUI();
            uiHealth.InitUI();
            uiCrypto.InitUI();
            uiScore.InitUI();
            uiTimer.InitUI();
        }
        #endregion

        #region Action Collect
        public void SoulCollected(int value)
        {
            uiScore.UpdateTextValue(value);
        }

        public void CryptoCollected(int value)
        {
            uiCrypto.UpdateTextValue(value);
        }

        public void DiamondCollected(int value)
        {
            uiDiamond.UpdateTextValue(value);
        }

        public void SymbolCollected()
        {
            uiUnboxed.AddSymbol();
        }

        public void SoulMissed()
        {
            uiHealth.Damaged();
        }
        #endregion

        #region Action Timer
        // Timer
        public void UpdateGameTimer(float timer, float maxTimer)
        {
            uiTimer.UpdateTimerText(timer);
            uiTimer.UpdateTimer(timer, maxTimer);
        }

        public void EndGameTimer()
        {
            //TODO: close bg
        }

        // Bonus
        public void StartBonusTimer()
        {
            uiUnboxed.EnableBonus();
        }

        public void UpdateBonusTimer(float timer, float maxTimer)
        {
            uiUnboxed.UpdateBonus(timer, maxTimer);
        }

        public void EndBonusTimer()
        {
            uiUnboxed.DisableBonus();
        }

        // Shield
        public void StartShieldTimer()
        {
            uiHealth.EnableShield();
        }

        public void EndShieldTimer()
        {
            uiHealth.DisableShield();
        }
        #endregion

        #region Button
        private void OnClickPause()
        {
            gameplayManager.OnPauseGameTimer?.Invoke();
        }
        #endregion
    }
}
