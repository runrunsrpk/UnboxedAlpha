using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIGameplay uiGameplay;
        public UIGameplay UIGameplay => uiGameplay;

        [SerializeField] private UIUpgrade uiUpgrade;
        public UIUpgrade UIUpgrade => uiUpgrade;

        [SerializeField] private UIEndgamePopup uiEndgamePopup;
        [SerializeField] private UIPausePopup uiPausePopup;

        [SerializeField] private UICountdown uiCountdown;
        [SerializeField] private UIHome uiHome;

        private GameplayManager gameplayManager;

        public void Initialize()
        {
            InitManager();
            EnableAction();

            uiHome.Show();
        }

        public void Terminate()
        {
            DisableAction();
        }

        private void InitManager()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
        }

        private void EnableAction()
        {
            gameplayManager.OnStartGame += StartGame;
            gameplayManager.OnRestartGame += RestartGame;
            gameplayManager.OnEndGame += EndGame;
            gameplayManager.OnExitGame += ExitGame;

            gameplayManager.OnSoulCollected += SoulCollected;
            gameplayManager.OnCryptoCollected += CryptoCollected;
            gameplayManager.OnDiamondCollected += DiamondCollected;
            gameplayManager.OnSymbolCollected += SymbolCollected;
            gameplayManager.OnSoulMissed += SoulMissed;

            gameplayManager.OnStartCountdownTimer += StartCountdownTimer;
            gameplayManager.OnUpdateCountdownTimer += UpdateCountdownTimer;
            gameplayManager.OnEndCountdownTimer += EndCountdownTimer;

            gameplayManager.OnUpdateGameTimer += UpdateGameTimer;
            gameplayManager.OnPauseGameTimer += PauseGameTimer;
            gameplayManager.OnEndGameTimer += EndGameTimer;

            gameplayManager.OnStartBonusTimer += StartBonusTimer;
            gameplayManager.OnUpdateBonusTimer += UpdateBonusTimer;
            gameplayManager.OnEndBonusTimer += EndBonusTimer;

            gameplayManager.OnStartShieldTimer += StartShieldTimer;
            gameplayManager.OnEndShieldTimer += EndShieldTimer;
        }

        private void DisableAction()
        {
            gameplayManager.OnStartGame -= StartGame;
            gameplayManager.OnRestartGame -= RestartGame;
            gameplayManager.OnEndGame -= EndGame;
            gameplayManager.OnExitGame -= ExitGame;

            gameplayManager.OnSoulCollected -= SoulCollected;
            gameplayManager.OnCryptoCollected -= CryptoCollected;
            gameplayManager.OnDiamondCollected -= DiamondCollected;
            gameplayManager.OnSymbolCollected -= SymbolCollected;
            gameplayManager.OnSoulMissed -= SoulMissed;

            gameplayManager.OnStartCountdownTimer -= StartCountdownTimer;
            gameplayManager.OnUpdateCountdownTimer -= UpdateCountdownTimer;
            gameplayManager.OnEndCountdownTimer -= EndCountdownTimer;

            gameplayManager.OnUpdateGameTimer -= UpdateGameTimer;
            gameplayManager.OnPauseGameTimer -= PauseGameTimer;
            gameplayManager.OnEndGameTimer -= EndGameTimer;


            gameplayManager.OnStartBonusTimer -= StartBonusTimer;
            gameplayManager.OnUpdateBonusTimer -= UpdateBonusTimer;
            gameplayManager.OnEndBonusTimer -= EndBonusTimer;

            gameplayManager.OnStartShieldTimer -= StartShieldTimer;
            gameplayManager.OnEndShieldTimer -= EndShieldTimer;
        }

        #region Action Game
        private void StartGame()
        {
            uiHome.Hide();
        }

        private void RestartGame()
        {
            uiPausePopup.Hide();
            uiEndgamePopup.Hide();
        }

        private void EndGame()
        {
            uiEndgamePopup.Show();

            //TODO: Save data to somewhere
            uiEndgamePopup.UpdatePopup(uiGameplay.Score, uiGameplay.Crypto);
        }

        private void ExitGame()
        {
            uiHome.Show();
            uiGameplay.Hide();
            uiEndgamePopup.Hide();
            uiPausePopup.Hide();
        }
        #endregion

        #region Action Collect
        private void SoulCollected(int value)
        {
            uiGameplay.SoulCollected(value);
        }

        private void CryptoCollected(int value)
        {
            uiGameplay.CryptoCollected(value);
        }

        private void DiamondCollected(int value)
        {
            uiGameplay.DiamondCollected(value);
        }

        private void SymbolCollected()
        {
            uiGameplay.SymbolCollected();
        }

        private void SoulMissed()
        {
            uiGameplay.SoulMissed();
        }
        #endregion

        #region Action Timer
        // Countdown
        private void StartCountdownTimer()
        {
            uiCountdown.Show();

            if (gameplayManager.IsStart == false)
            {
                uiGameplay.Show();
            }
            else
            {
                uiPausePopup.Hide();
            }
        }

        private void UpdateCountdownTimer(float timer)
        {
            uiCountdown.UpdateText(timer);
        }

        private void EndCountdownTimer()
        {
            uiCountdown.Hide();
        }

        // Timer
        private void UpdateGameTimer(float timer, float maxTimer)
        {
            uiGameplay.UpdateGameTimer(timer, maxTimer);
        }

        private void PauseGameTimer()
        {
            uiPausePopup.Show();
        }

        private void EndGameTimer()
        {
            uiEndgamePopup.Show();
        }

        // Bonus
        public void StartBonusTimer()
        {
            uiGameplay.StartBonusTimer();
        }

        public void UpdateBonusTimer(float timer, float maxTimer)
        {
            uiGameplay.UpdateBonusTimer(timer, maxTimer);
        }

        public void EndBonusTimer()
        {
            uiGameplay.EndBonusTimer();
        }

        // Shield
        public void StartShieldTimer()
        {
            uiGameplay.StartShieldTimer();
        }

        public void EndShieldTimer()
        {
            uiGameplay.EndShieldTimer();
        }
        #endregion
    }
}

