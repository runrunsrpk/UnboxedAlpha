using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class GameplayData
    {
        public int health;
        public int shield;
        public int pureSoul;
        public int colorSoul;
        public int crypto;
        public int diamond;
        public float bonus;
        public int clock;
        public float timer;
    }

    public class GameplayManager : MonoBehaviour
    {
        public Action OnStartGame;
        public Action OnRestartGame;
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
        //[SerializeField] private float maxGameTimer;
        //[SerializeField] private float maxBonusTimer;
        //[SerializeField] private float maxShieldTimer;

        private bool isCountdown;
        private float countdownTimer;
        private bool isStart;
        private bool isPause;
        private float gameTimer;
        private bool isBonus;
        private float bonusTimer;
        private bool isShield;
        private float shieldTimer;

        private const float TICK_TIMER_MAX = 0.1f; // 1f = 1 sec and 0.1f = 1 millisec
        private const float TIMER_MULTIPLIER = 10f; // covert millisec to second

        private float tickCountdownTimer;
        private float tickGameTimer;
        private float tickBonusTimer;
        private float tickShieldTimer;

        private GameManager gameManager;
        private SpawnerManager spawnerManager;
        private PlayerManager playerManager;

        [SerializeField] private GameplayValueData gameplayValueData;

        private GameplayData baseGameplayData;
        private GameplayData gameplayData;
        public GameplayData GameplayData => gameplayData;

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
            //gameTimer = maxGameTimer * TIMER_MULTIPLIER;
            //bonusTimer = maxBonusTimer * TIMER_MULTIPLIER;
            //shieldTimer = maxShieldTimer * TIMER_MULTIPLIER;

            gameTimer = gameplayData.timer * TIMER_MULTIPLIER;
            bonusTimer = gameplayData.bonus * TIMER_MULTIPLIER;
            shieldTimer = gameplayData.shield * TIMER_MULTIPLIER;
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;
            spawnerManager = gameManager.SpawnerManager;
            playerManager = gameManager.PlayerManager;
        }

        private void InitGameplayData()
        {
            baseGameplayData = new GameplayData()
            {
                health = 1,
                shield = 5,
                pureSoul = 1,
                colorSoul = 3,
                crypto = 1,
                diamond = 1,
                bonus = 10f,
                clock = 5,
                timer = 60f,
            };
  
            gameplayData = new GameplayData()
            {
                health = baseGameplayData.health + gameplayValueData.healthValue[playerManager.GetUpgradeLevel(UpgradeType.Health)],
                shield = baseGameplayData.shield + gameplayValueData.shieldTimerValue[playerManager.GetUpgradeLevel(UpgradeType.Shield)],
                pureSoul = baseGameplayData.pureSoul + gameplayValueData.pureSoulValue[playerManager.GetUpgradeLevel(UpgradeType.PureSoul)],
                colorSoul = baseGameplayData.colorSoul + gameplayValueData.colorSoulValue[playerManager.GetUpgradeLevel(UpgradeType.ColorSoul)],
                crypto = baseGameplayData.crypto + gameplayValueData.cryptoValue[playerManager.GetUpgradeLevel(UpgradeType.Crypto)],
                diamond = baseGameplayData.diamond,
                bonus = baseGameplayData.bonus + gameplayValueData.bonusTimerValue[playerManager.GetUpgradeLevel(UpgradeType.Bonus)],
                clock = baseGameplayData.clock + gameplayValueData.clockValue[playerManager.GetUpgradeLevel(UpgradeType.Clock)],
                timer = baseGameplayData.timer + gameplayValueData.gameTimerValue[playerManager.GetUpgradeLevel(UpgradeType.Timer)],
            };
        }

        private void EnableAction()
        {
            OnStartGame += StartGame;
            OnRestartGame += RestartGame;
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
            OnRestartGame -= RestartGame;
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
            InitGameplayData();
            InitVariable();

            OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, gameplayData.timer);
            OnStartCountdownTimer?.Invoke();
        }

        private void RestartGame()
        {
            EndGame();
            StartGame();
        }

        private void EndGame()
        {
            isCountdown = false;
            isPause = false;
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
            gameTimer = gameplayData.timer * TIMER_MULTIPLIER;
            OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, gameplayData.timer);
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
                        OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, gameplayData.timer);
                    }
                }
            }
            else
            {
                OnEndGameTimer?.Invoke();
                OnEndGame?.Invoke();
            }
        }

        private void EndGameTimer()
        {
            OnUpdateGameTimer?.Invoke(0f, gameplayData.timer);
            tickGameTimer = 0f;
        }

        // Bonus
        private void StartBonusTimer()
        {
            isBonus = true;
            bonusTimer = gameplayData.bonus * TIMER_MULTIPLIER;
            OnUpdateBonusTimer?.Invoke(bonusTimer / TIMER_MULTIPLIER, gameplayData.bonus);
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
                        OnUpdateBonusTimer?.Invoke(bonusTimer / TIMER_MULTIPLIER, gameplayData.bonus);
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
            OnUpdateBonusTimer?.Invoke(0f, gameplayData.bonus);
            isBonus = false;
            bonusTimer = 0f;
        }

        // Shield
        private void StartShieldTimer()
        {
            isShield = true;
            shieldTimer = gameplayData.shield * TIMER_MULTIPLIER;
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
            gameTimer = ((gameTimer / TIMER_MULTIPLIER) + value > gameplayData.timer) ?
                gameplayData.timer * TIMER_MULTIPLIER : gameTimer + ( value * TIMER_MULTIPLIER);

            OnUpdateGameTimer?.Invoke(gameTimer / TIMER_MULTIPLIER, gameplayData.timer);
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

