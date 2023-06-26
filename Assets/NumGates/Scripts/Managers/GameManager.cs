using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameplayManager GameplayManager { get; private set; }
        public SpawnerManager SpawnerManager { get; private set; }
        public UIManager UIManager { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            GameplayManager = GetComponentInChildren<GameplayManager>();
            SpawnerManager = GetComponentInChildren<SpawnerManager>();
            UIManager = GetComponentInChildren<UIManager>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            GameplayManager.Initialize();
            SpawnerManager.Initialize();
            UIManager.Initialize();
        }

        private void Terminate()
        {
            GameplayManager.Terminate();
            SpawnerManager.Terminate();
            UIManager.Terminate();
        }
    }
}


