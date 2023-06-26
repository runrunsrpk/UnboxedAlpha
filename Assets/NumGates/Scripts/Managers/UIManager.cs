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

        private GameplayManager gameplayManager;

        public void Initialize()
        {
            InitManager();

            EnableAction();

            uiGameplay.Initialize();
        }

        public void Terminate()
        {
            DisableAction();

            uiGameplay.Terminate();
        }

        private void InitManager()
        {
            gameplayManager = GameManager.Instance.GameplayManager;
        }

        private void EnableAction()
        {
            gameplayManager.OnEndGameTimer += EndGameTimer;
        }

        private void DisableAction()
        {

        }

        private void EndGameTimer()
        {
            uiEndgamePopup.Show();
        }
    }
}

