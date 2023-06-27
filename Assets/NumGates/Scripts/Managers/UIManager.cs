using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIGameplay uiGameplay;
        public UIGameplay UIGameplay => uiGameplay;

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
            //uiGameplay.Initialize();
        }

        public void Terminate()
        {
            DisableAction();

            //uiGameplay.Terminate();
        }

        private void InitManager()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
        }

        private void EnableAction()
        {
            gameplayManager.OnStartCountdownTimer += StartCountdownTimer;
            gameplayManager.OnEndCountdownTimer += EndCountdownTimer;

            gameplayManager.OnPauseGameTimer += PauseGameTimer;
            gameplayManager.OnEndGameTimer += EndGameTimer;
        }

        private void DisableAction()
        {

        }

        #region Action Timer
        private void StartCountdownTimer()
        {
            uiHome.Hide();
            uiCountdown.Show();

            if(gameplayManager.IsStart == false)
            {
                uiGameplay.Show();
            }
            else
            {
                uiPausePopup.Hide();
            }
        }

        private void EndCountdownTimer()
        {
            uiCountdown.Hide();
        }

        private void PauseGameTimer()
        {
            uiPausePopup.Show();
        }

        private void EndGameTimer()
        {
            uiEndgamePopup.Show();
        }
        #endregion
    }
}

