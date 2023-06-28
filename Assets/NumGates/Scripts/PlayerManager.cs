using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class PlayerData
    {
        public int highscore;
        public int crypto;
        public int diamond;

        public int upgradeHealth;
        public int upgradeShield;
        public int upgradePureSoul;
        public int upgradeColorSoul;
        public int upgradeCrypto;
        public int upgradeDiamond;
        public int upgradeBonus;
        public int upgradeClock;
        public int upgradeTimer;
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

        public int GetUpgradeHealth()
        {
            return playerData.upgradeHealth;
        }

        public int GetUpgradeShield()
        {
            return playerData.upgradeShield;
        }

        public int GetUpgradePureSoul()
        {
            return playerData.upgradePureSoul;
        }

        public int GetUpgradeColorSoul()
        {
            return playerData.upgradeColorSoul;
        }

        public int GetUpgradeCrypto()
        {
            return playerData.upgradeCrypto;
        }

        public int GetUpgradeDiamond()
        {
            return playerData.upgradeDiamond;
        }

        public int GetUpgradeBonus()
        {
            return playerData.upgradeBonus;
        }

        public int GetUpgradeClock()
        {
            return playerData.upgradeClock;
        }

        public int GetUpgradeTimer()
        {
            return playerData.upgradeTimer;
        }
        #endregion
    }
}

