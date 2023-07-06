using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public enum SpawnType
    {
        PureSoul,
        ColorSoul,
        MadSoul,
        Crypto,
        //Diamond,
        Clock,
        Symbol,
        Shield
}

    public class SpawnerManager : MonoBehaviour
    {
        public Action OnUpdateWaveTimer;

        [Header("Spawinng Initial")]
        [SerializeField] private float frameOffset;
        [SerializeField] private GameObject[] spawnPrefs;

        [Header("Spawning Timer")]
        [SerializeField] private float waveTimer;

        [Header("Spawning Conditions")]
        [SerializeField] private int minSpawning;
        [SerializeField] private int maxSpawning;
        [SerializeField] private int spawningLevel;         // Condition to upgrade spawner
        [SerializeField] private float baseSpawningSpeed;
        [SerializeField] private float bonusSpawningSpeed;
        [SerializeField] private float spawningSpeed;       // Speed between each wave of spawning
        [SerializeField] private float spawningDelaySpeed;  // Speed between each spawning

        private const float TIMER_MULTIPLIER = 10f; // covert millisec to second

        [Header("Spawning Rate")]
        [Range(0f, 1f)] [SerializeField] private float spawningSoulRate;
        [Range(0f, 1f)] [SerializeField] private float spawningColorSoulRate;
        [Range(0f, 1f)] [SerializeField] private float spawningMadSoulRate;

        [Header("Item Amount")]
        [SerializeField] private int itemAmount;

        private GameManager gameManager;
        private UIManager uiManager;
        private GameplayManager gameplayManager;

        private GameObject parent;

        // Order in layer
        private int maxLayer = 900;
        private int layer = 0;

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
            parent = new GameObject("SpawnerParent");

            spawningSpeed = baseSpawningSpeed * TIMER_MULTIPLIER;
            waveTimer = baseSpawningSpeed * TIMER_MULTIPLIER;
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;

            uiManager = gameManager.UIManager;
            gameplayManager = gameManager.GameplayManager;
        }

        private void EnableAction()
        {
            OnUpdateWaveTimer += UpdateWaveTimer;

            gameplayManager.OnStartGame += StartGame;
            gameplayManager.OnRestartGame += RestartGame;
            gameplayManager.OnExitGame += ExitGame;

            gameplayManager.OnStartBonusTimer += StartBonusTimer;
            gameplayManager.OnEndBonusTimer += EndBonusTimer;
        }

        private void DisableAction()
        {
            OnUpdateWaveTimer -= UpdateWaveTimer;

            gameplayManager.OnStartGame -= StartGame;
            gameplayManager.OnRestartGame -= RestartGame;
            gameplayManager.OnExitGame -= ExitGame;

            gameplayManager.OnStartBonusTimer -= StartBonusTimer;
            gameplayManager.OnEndBonusTimer -= EndBonusTimer;
        }

        private void UpdateWaveTimer()
        {
            if(waveTimer - 1 > 0f)
            {
                waveTimer--;
            }
            else
            {
                //RandomSpawning();
                StartCoroutine(Spawning());
                waveTimer = spawningSpeed;
            }
        }

        private void RandomSpawnPosition(GameObject spawnedItem)
        {
            Camera camera = Camera.main;

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Calculate frame size
            Vector2 leftFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.LeftFrame, frameOffset);
            Vector2 rightFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.RightFrame, frameOffset);
            Vector2 topFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.TopFrame, frameOffset);
            Vector2 bottomFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.BottomFrame, frameOffset);

            Vector2 contentTopFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.ContentTopFrame, frameOffset);
            Vector2 contentBottomFrameSize = UnboxedUtils.CalculateUISize(uiManager.UIGameplay.ContentBottomFrame, frameOffset);

            // Create frame offset
            float leftOffset = leftFrameSize.x;
            float rightOffset = rightFrameSize.x;
            float topOffset = topFrameSize.y + contentTopFrameSize.y;
            float bottomOffset = bottomFrameSize.y + contentBottomFrameSize.y;

            // Calculate spawned item size
            Vector2 spawnedSize = UnboxedUtils.CalculateSpriteSize(spawnedItem);

            // Create spawned item offset
            float itemWidthOffset = spawnedSize.x / 2f;
            float itemHeightOffset = spawnedSize.y / 2f;

            // Fix spawn position
            //int randomId = UnityEngine.Random.Range(0, 4);
            //Vector3[] randomSet = new Vector3[4];
            //randomSet[0] = new Vector3(leftOffset + itemWidthOffset, topOffset + itemHeightOffset, 0f);
            //randomSet[1] = new Vector3(screenWidth - rightOffset - itemWidthOffset, topOffset + itemHeightOffset, 0f);
            //randomSet[2] = new Vector3(leftOffset + itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);
            //randomSet[3] = new Vector3(screenWidth - rightOffset - itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);
            //Vector3 randomPosition = camera.ScreenToWorldPoint(randomSet[randomId]);

            // Random spawn position
            float randomWidth = UnityEngine.Random.Range(leftOffset + itemWidthOffset, screenWidth - rightOffset - itemWidthOffset);
            float randomHeight = UnityEngine.Random.Range(topOffset + itemHeightOffset, screenHeight - bottomOffset - itemHeightOffset);

            Vector3 randomPosition = camera.ScreenToWorldPoint(new Vector3(randomWidth, randomHeight, 0f));

            randomPosition.y = GetReverseValue(randomPosition.y);
            randomPosition.z = GetLayerOrder();

            spawnedItem.transform.position = randomPosition;
        }

        #region Action Game
        private void StartGame()
        {
            InitVariable();
        }

        private void RestartGame()
        {
            Destroy(parent);
            StartGame();
        }

        private void ExitGame()
        {
            Destroy(parent);
        }
        #endregion

        #region Action Timer
        private void StartBonusTimer()
        {
            spawningSpeed = bonusSpawningSpeed * TIMER_MULTIPLIER;
        }

        private void EndBonusTimer()
        {
            spawningSpeed = baseSpawningSpeed * TIMER_MULTIPLIER;
        }
        #endregion

        #region Random
        private float RandomPercentValue()
        {
            return UnityEngine.Random.Range(0f, 1f);
        }

        private void SpawnCollectable(SpawnType type)
        {
            Collectable collectable = Instantiate(spawnPrefs[(int)type], parent.transform).GetComponent<Collectable>();
            RandomSpawnPosition(collectable.gameObject);
            InitSpawnedValue(type, collectable);
        }

        private IEnumerator Spawning()
        {
            int spawningAmount = UnityEngine.Random.Range(minSpawning, maxSpawning);
            float spawningDelay = ( (spawningSpeed / TIMER_MULTIPLIER) / 2f ) / spawningAmount;

            for (int i = 0; i < spawningAmount; i++)
            {
                float randomValue = RandomPercentValue();

                if (randomValue <= spawningSoulRate)
                {
                    SpawnSoul();
                }
                else
                {
                    SpawnItem();
                }

                yield return new WaitForSecondsRealtime(spawningDelay);
            }
        }

        private void InitSpawnedValue(SpawnType type, Collectable collectable)
        {
            switch (type)
            {
                case SpawnType.PureSoul:
                    collectable.InitValue(gameplayManager.GameplayData.pureSoul);
                    break;
                case SpawnType.ColorSoul:
                    collectable.InitValue(gameplayManager.GameplayData.colorSoul);
                    break;
                case SpawnType.MadSoul:
                    collectable.InitValue(0);
                    break;
                case SpawnType.Crypto:
                    collectable.InitValue(gameplayManager.GameplayData.crypto);
                    break;
                case SpawnType.Clock:
                    collectable.InitValue(gameplayManager.GameplayData.clock);
                    break;
                case SpawnType.Symbol:
                    collectable.InitValue(0);
                    break;
                case SpawnType.Shield:
                    collectable.InitValue(gameplayManager.GameplayData.shield);
                    break;
            }
        }

        private void SpawnSoul()
        {
            float randomValue = RandomPercentValue();
            SpawnCollectable(GetSoulType(randomValue));
        }

        private SpawnType GetSoulType(float value)
        {
            SpawnType type = SpawnType.PureSoul;

            if (!gameplayManager.IsBonus)
            {
                if(value <= spawningMadSoulRate + spawningColorSoulRate)
                {
                    type = (value <= spawningMadSoulRate) ? SpawnType.MadSoul : SpawnType.ColorSoul;
                }
            }
            else
            {
                type = (value <= spawningMadSoulRate + spawningColorSoulRate) 
                    ? SpawnType.ColorSoul : SpawnType.PureSoul;
            }

            return type;
        }

        private void SpawnItem()
        {
            int maxRandom = (!gameplayManager.IsBonus) ? itemAmount : itemAmount - 1;
            int randomValue = UnityEngine.Random.Range(0, maxRandom);
            SpawnCollectable(GetItemType(randomValue));
        }

        private SpawnType GetItemType(int value)
        {
            SpawnType type = value switch
            {
                0 => SpawnType.Crypto,
                1 => SpawnType.Clock,
                2 => SpawnType.Shield,
                3 => SpawnType.Symbol,
                _ => SpawnType.Crypto
            };

            return type;
        }
        #endregion

        #region 
        private float GetReverseValue(float value)
        {
            return -value;
        }

        private int GetLayerOrder()
        {
            layer = (layer + 2 < maxLayer) ? layer + 2 : maxLayer ;
            return layer;
        }
        #endregion
    }
}


