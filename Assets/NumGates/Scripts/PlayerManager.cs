using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public enum UpgradeType
    {
        Health,
        Shield,
        PureSoul,
        ColorSoul,
        Crypto,
        Diamond,
        Bonus,
        Clock,
        Timer
    }

    public class PlayerData
    {
        public int highscore;
        public int crypto;
        public int diamond;
        public int[] updradeLevels = new int[9];

        //public int upgradeHealth;
        //public int upgradeShield;
        //public int upgradePureSoul;
        //public int upgradeColorSoul;
        //public int upgradeCrypto;
        //public int upgradeDiamond;
        //public int upgradeBonus;
        //public int upgradeClock;
        //public int upgradeTimer;
    }

    public class PlayerManager : MonoBehaviour
    {
        private GameManager gameManager;
        private GameplayManager gameplayManager;

        private PlayerData playerData;

        public void Initialize()
        {
            InitManager();
            InitPlayer();
            EnableAction();
        }

        public void Terminate()
        {
            DisableAction();
        }

        private void InitManager()
        {
            gameManager = GameManager.Instance;

            gameplayManager = gameManager.GameplayManager;
        }

        private void InitPlayer()
        {
            playerData = new PlayerData();

            playerData.highscore = 99;
            playerData.crypto = 999999;
            playerData.diamond = 9999;

            playerData.updradeLevels[(int)UpgradeType.Health] = 4;
            playerData.updradeLevels[(int)UpgradeType.Shield] = 1;
            playerData.updradeLevels[(int)UpgradeType.PureSoul] = 4;
            playerData.updradeLevels[(int)UpgradeType.ColorSoul] = 3;
            playerData.updradeLevels[(int)UpgradeType.Crypto] = 2;
            playerData.updradeLevels[(int)UpgradeType.Diamond] = 0;
            playerData.updradeLevels[(int)UpgradeType.Bonus] = 2;
            playerData.updradeLevels[(int)UpgradeType.Clock] = 2;
            playerData.updradeLevels[(int)UpgradeType.Timer] = 2;

            //TODO: Load data from save
        }

        private void EnableAction()
        {

        }

        private void DisableAction()
        {

        }

        #region Get
        public int GetHighscore()
        {
            return playerData.highscore;
        }

        public int GetCrypto()
        {
            return playerData.crypto;
        }

        public int GetDiamond()
        {
            return playerData.diamond;
        }

        public int GetUpgradeLevel(UpgradeType type)
        {
            return playerData.updradeLevels[(int)type];
        }
        #endregion
    }
}

