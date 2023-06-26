using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class GameplayManager : MonoBehaviour
    {
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
        public Action OnStartGameTimer;
        public Action<float, float> OnUpdateGameTimer;
        public Action OnEndGameTimer;

        public Action OnStartBonusTimer;
        public Action<float, float> OnUpdateBonusTimer;
        public Action OnEndBonusTimer;

        public Action OnStartShieldTimer;
        public Action OnEndShieldTimer;

        public bool IsStart => isStart;
        public bool IsBonus => isBonus;
        public bool IsShield => isShield;

        [SerializeField] private float maxGameTimer;
        [SerializeField] private float maxBonusTimer;
        [SerializeField] private float maxShieldTimer;

        [SerializeField] private bool isStart;
        [SerializeField] private float gameTimer;
        [SerializeField] private bool isBonus;
        [SerializeField] private float bonusTimer;
        [SerializeField] private bool isShield;
        [SerializeField] private float shieldTimer;

        private const float TICK_TIMER_MAX = 0.1f; // 1f = 1 sec and 0.1f = 1 millisec
        private const float TIMER_MULTIPLIER = 10f; // covert millisec to second

        private float tickGameTimer;
        private float tickBonusTimer;
        private float tickShieldTimer;

        private GameManager gameManager;
        private SpawnerManager spawnerManager;

        private void Update()
        {
            UpdateGameTimer();
            UpdateBonusTimer();
            UpdateShieldTimer();
        }

        #region Initiailize
        public void Initialize()
        {
            InitManager();
            InitVariable();

            EnableAction();
        }

        public void Terminate()
        {
            DisableAction();
        }

        private void InitVariable()
        {
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
            OnClockCollected += ClockCollected;
            OnShieldCollected += ShieldCollected;
            OnMadSoulCollected += MadSoulCollected;

            OnEndGameTimer += EndGameTimer;

            OnStartBonusTimer += StartBonusTimer;
            OnEndBonusTimer += EndBonusTimer;

            OnStartShieldTimer += StartShieldTimer;
            OnEndShieldTimer += EndShieldTimer;
        }

        private void DisableAction()
        {
            
        }
        #endregion

        #region Action Timer
        private void UpdateGameTimer()
        {
            if (isStart == false) return;

            if (gameTimer - 1 > 0f)
            {
                tickGameTimer += Time.deltaTime;

                //Debug.Log($"Tick: {tickGameTimer} | Delta: {Time.deltaTime}");

                if (tickGameTimer >= TICK_TIMER_MAX)
                {
                    tickGameTimer -= TICK_TIMER_MAX;
                    gameTimer--;

                    //OnUpdateGameTimer?.Invoke(gameTimer, maxGameTimer);
                    spawnerManager.OnUpdateWaveTimer?.Invoke();

                    if (gameTimer % TIMER_MULTIPLIER == 0)
                    {
                        OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, maxGameTimer);
                    }
                }
            }
            else
            {
                tickGameTimer = 0f;
                // Invoke game stop action
                OnEndGameTimer?.Invoke();
            }
        }

        private void EndGameTimer()
        {
            isStart = false;
        }

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

            if (bonusTimer - 1 > 0f)
            {
                tickBonusTimer += Time.deltaTime;

                if (tickBonusTimer >= TICK_TIMER_MAX)
                {
                    tickBonusTimer -= TICK_TIMER_MAX;
                    bonusTimer--;

                    //OnUpdateBonusTimer?.Invoke(bonusTimer, maxBonusTimer);

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
            isBonus = false;
            bonusTimer = 0f;
        }

        private void UpdateShieldTimer()
        {
            if (isStart == false) return;

            if (isShield == false) return;

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

        private void StartShieldTimer()
        {
            isShield = true;
            shieldTimer = maxShieldTimer * TIMER_MULTIPLIER;
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
                OnEndGameTimer?.Invoke();
            }
        }
        #endregion
    }
}

