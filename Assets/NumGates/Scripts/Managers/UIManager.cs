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

            gameplayManager.OnEndGameTimer += EndGameTimer;
        }

        private void DisableAction()
        {

        }

        #region Action Timer
        private void StartCountdownTimer()
        {
            uiHome.Hide();
            uiGameplay.Show();
            uiCountdown.Show();
        }

        private void EndCountdownTimer()
        {
            uiCountdown.Hide();
        }

        private void EndGameTimer()
        {
            uiEndgamePopup.Show();
        }
        #endregion
    }
}

