using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class GameplayManager : MonoBehaviour
    {
        public Action OnStartGame;
        public Action OnEndGame;
        public Action OnExitGame;

        [Header("Collect")]
        public Action<int> OnSoulCollected;
        public Action<int> OnCryptoCollected;
        public Action<int> OnDiamondCollected;
        public Action<int> OnClockCollected;
        public Action OnSymbolCollected;
        public Action OnShieldCollected;
        public Action OnMadSoulCollected;
        public Action OnSoulMissed;

        [Header("Timer")]
        public Action OnStartCountdownTimer;
        public Action<float> OnUpdateCountdownTimer;
        public Action OnEndCountdownTimer;

        public Action OnStartGameTimer;
        public Action OnPauseGameTimer;
        public Action OnResumeGameTimer;
        public Action<float, float> OnUpdateGameTimer;
        public Action OnEndGameTimer;

        public Action OnStartBonusTimer;
        public Action<float, float> OnUpdateBonusTimer;
        public Action OnEndBonusTimer;

        public Action OnStartShieldTimer;
        public Action OnEndShieldTimer;

        public bool IsStart => isStart;
        public bool IsPause => isPause;
        public bool IsBonus => isBonus;
        public bool IsShield => isShield;

        [SerializeField] private float maxCountdownTimer;
        [SerializeField] private float maxGameTimer;
        [SerializeField] private float maxBonusTimer;
        [SerializeField] private float maxShieldTimer;

        [SerializeField] private bool isCountdown;
        [SerializeField] private float countdownTimer;
        [SerializeField] private bool isStart;
        [SerializeField] private bool isPause;
        [SerializeField] private float gameTimer;
        [SerializeField] private bool isBonus;
        [SerializeField] private float bonusTimer;
        [SerializeField] private bool isShield;
        [SerializeField] private float shieldTimer;

        private const float TICK_TIMER_MAX = 0.1f; // 1f = 1 sec and 0.1f = 1 millisec
        private const float TIMER_MULTIPLIER = 10f; // covert millisec to second

        private float tickCountdownTimer;
        private float tickGameTimer;
        private float tickBonusTimer;
        private float tickShieldTimer;

        private GameManager gameManager;
        private SpawnerManager spawnerManager;

        private void Update()
        {
            UpdateCountdownTimer();
            UpdateGameTimer();
            UpdateBonusTimer();
            UpdateShieldTimer();
        }

        #region Initiailize
        public void Initialize()
        {
            InitManager();
            EnableAction();
        }

        public void Terminate()
        {
            DisableAction();
        }

        private void InitVariable()
        {
            countdownTimer = maxCountdownTimer * TIMER_MULTIPLIER;
            gameTimer = maxGameTimer * TIMER_MULTIPLIER;
            bonusTimer = maxBonusTimer * TIMER_MULTIPLIER;
            shieldTimer = maxShieldTimer * TIMER_MULTIPLIER;
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;
            spawnerManager = gameManager.SpawnerManager;
        }

        private void EnableAction()
        {
            OnStartGame += StartGame;
            OnEndGame += EndGame;
            OnExitGame += ExitGame;

            OnClockCollected += ClockCollected;
            OnShieldCollected += ShieldCollected;
            OnMadSoulCollected += MadSoulCollected;

            OnStartCountdownTimer += StartCountdownTimer;
            OnEndCountdownTimer += EndCountdownTimer;

            OnStartGameTimer += StartGameTimer;
            OnPauseGameTimer += PauseGameTimer;
            OnResumeGameTimer += ResumeGameTimer;
            OnEndGameTimer += EndGameTimer;

            OnStartBonusTimer += StartBonusTimer;
            OnEndBonusTimer += EndBonusTimer;

            OnStartShieldTimer += StartShieldTimer;
            OnEndShieldTimer += EndShieldTimer;
        }

        private void DisableAction()
        {
            OnStartGame -= StartGame;
            OnEndGame -= EndGame;
            OnExitGame -= ExitGame;

            OnClockCollected -= ClockCollected;
            OnShieldCollected -= ShieldCollected;
            OnMadSoulCollected -= MadSoulCollected;

            OnStartCountdownTimer -= StartCountdownTimer;
            OnEndCountdownTimer -= EndCountdownTimer;

            OnStartGameTimer -= StartGameTimer;
            OnPauseGameTimer -= PauseGameTimer;
            OnResumeGameTimer -= ResumeGameTimer;
            OnEndGameTimer -= EndGameTimer;

            OnStartBonusTimer -= StartBonusTimer;
            OnEndBonusTimer -= EndBonusTimer;

            OnStartShieldTimer -= StartShieldTimer;
            OnEndShieldTimer -= EndShieldTimer;
        }
        #endregion

        #region Action Game
        private void StartGame()
        {
            InitVariable();

            OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, maxGameTimer);
            OnStartCountdownTimer?.Invoke();
        }

        private void EndGame()
        {
            isStart = false;
            isBonus = false;
            isShield = false;
        }

        private void ExitGame()
        {
            isCountdown = false;
            isPause = false;
            isStart = false;
            isBonus = false;
            isShield = false;
        }
        #endregion

        #region Action Timer
        private void StartCountdownTimer()
        {
            isCountdown = true;
            countdownTimer = maxCountdownTimer * TIMER_MULTIPLIER;
            OnUpdateCountdownTimer?.Invoke(countdownTimer / TIMER_MULTIPLIER);
        }

        private void UpdateCountdownTimer()
        {
            if (isCountdown == false) return;

            if (countdownTimer - 1 > 0f)
            {
                tickCountdownTimer += Time.deltaTime;

                if (tickCountdownTimer >= TICK_TIMER_MAX)
                {
                    tickCountdownTimer -= TICK_TIMER_MAX;
                    countdownTimer--;

                    if (countdownTimer % TIMER_MULTIPLIER == 0)
                    {
                        OnUpdateCountdownTimer?.Invoke(countdownTimer / TIMER_MULTIPLIER);
                    }
                }
            }
            else
            {
                OnEndCountdownTimer?.Invoke();
            }
        }

        private void EndCountdownTimer()
        {
            tickCountdownTimer = 0f;
            isCountdown = false;

            if(isStart == false)
            {
                OnStartGameTimer?.Invoke();
            }
            else
            {
                OnResumeGameTimer?.Invoke();
            }
        }

        // Game
        private void StartGameTimer()
        {
            isStart = true;
            gameTimer = maxGameTimer * TIMER_MULTIPLIER;
            OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, maxGameTimer);
        }

        private void PauseGameTimer()
        {
            isPause = true;
        }

        private void ResumeGameTimer()
        {
            isPause = false;
        }

        private void UpdateGameTimer()
        {
            if (isStart == false) return;

            if (isPause == true) return;

            if (gameTimer - 1 > 0f)
            {
                tickGameTimer += Time.deltaTime;

                if (tickGameTimer >= TICK_TIMER_MAX)
                {
                    tickGameTimer -= TICK_TIMER_MAX;
                    gameTimer--;

                    spawnerManager.OnUpdateWaveTimer?.Invoke();

                    if (gameTimer % TIMER_MULTIPLIER == 0)
                    {
                        OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, maxGameTimer);
                    }
                }
            }
            else
            {
                OnEndGameTimer?.Invoke();
            }
        }

        private void EndGameTimer()
        {
            OnUpdateGameTimer?.Invoke(0f, maxGameTimer);
            tickGameTimer = 0f;
            isStart = false;
        }

        // Bonus
        private void StartBonusTimer()
        {
            isBonus = true;
            bonusTimer = maxBonusTimer * TIMER_MULTIPLIER;
            OnUpdateBonusTimer?.Invoke(bonusTimer / TIMER_MULTIPLIER, maxBonusTimer);
        }

        private void UpdateBonusTimer()
        {
            if (isStart == false) return;

            if (isBonus == false) return;

            if (isPause == true) return;

            if (bonusTimer - 1 > 0f)
            {
                tickBonusTimer += Time.deltaTime;

                if (tickBonusTimer >= TICK_TIMER_MAX)
                {
                    tickBonusTimer -= TICK_TIMER_MAX;
                    bonusTimer--;

                    if (bonusTimer % TIMER_MULTIPLIER == 0)
                    {
                        OnUpdateBonusTimer?.Invoke(bonusTimer / TIMER_MULTIPLIER, maxBonusTimer);
                    }
                }
            }
            else
            {
                OnEndBonusTimer?.Invoke();
            }
        }

        private void EndBonusTimer()
        {
            OnUpdateBonusTimer?.Invoke(0f, maxBonusTimer);
            isBonus = false;
            bonusTimer = 0f;
        }

        // Shield
        private void StartShieldTimer()
        {
            isShield = true;
            shieldTimer = maxShieldTimer * TIMER_MULTIPLIER;
        }

        private void UpdateShieldTimer()
        {
            if (isStart == false) return;

            if (isShield == false) return;

            if (isPause == true) return;

            if (shieldTimer - 1 > 0f)
            {
                tickShieldTimer += Time.deltaTime;

                if (tickShieldTimer >= TICK_TIMER_MAX)
                {
                    tickShieldTimer -= TICK_TIMER_MAX;
                    shieldTimer--;
                }
            }
            else
            {
                OnEndShieldTimer?.Invoke();
            }
        }

        private void EndShieldTimer()
        {
            isShield = false;
            shieldTimer = 0f;
        }
        #endregion

        #region Action Collect
        private void ClockCollected(int value)
        {
            gameTimer = ((gameTimer / TIMER_MULTIPLIER) + value > maxGameTimer) ?
                maxGameTimer * TIMER_MULTIPLIER : gameTimer + ( value * TIMER_MULTIPLIER);

            OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, maxGameTimer);
        }

        private void ShieldCollected()
        {
            if(isShield == false)
            {
                OnStartShieldTimer?.Invoke();
            }
        }

        private void MadSoulCollected()
        {
            if(isShield == false)
            {
                OnEndGame?.Invoke();
            }
        }
        #endregion
    }
}

