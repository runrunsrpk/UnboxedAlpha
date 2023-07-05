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
        //Diamond,
        Bonus,
        Clock,
        Timer
    }

    public static class PlayerPrefsKey
    {
        public static readonly string Highscore = "Highscore";
        public static readonly string Crypto = "Crypto";
        public static readonly string Upgrades = "Upgrades";
    }

    public class PlayerData
    {
        public int highscore;
        public int crypto;
        //public int diamond;
        public int[] updradeLevels = new int[8];
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

            if(PlayerPrefs.HasKey(PlayerPrefsKey.Highscore))
            {
                //playerData.highscore = 99;
                playerData.highscore = PlayerPrefs.GetInt(PlayerPrefsKey.Highscore);
            }

            if(PlayerPrefs.HasKey(PlayerPrefsKey.Crypto))
            {
                //playerData.crypto = 999999;
                playerData.crypto = PlayerPrefs.GetInt(PlayerPrefsKey.Crypto);
            }

            //playerData.diamond = 9999;

            if (PlayerPrefs.HasKey(PlayerPrefsKey.Upgrades))
            {
                string[] upgradeLevels = PlayerPrefs.GetString(PlayerPrefsKey.Upgrades).Split('/');

                for(int i = 0; i < upgradeLevels.Length; i++)
                {
                    playerData.updradeLevels[i] = int.Parse(upgradeLevels[i]);
                }
            }

            //playerData.updradeLevels[(int)UpgradeType.Health] = 4;
            //playerData.updradeLevels[(int)UpgradeType.Shield] = 1;
            //playerData.updradeLevels[(int)UpgradeType.PureSoul] = 4;
            //playerData.updradeLevels[(int)UpgradeType.ColorSoul] = 3;
            //playerData.updradeLevels[(int)UpgradeType.Crypto] = 2;
            //playerData.updradeLevels[(int)UpgradeType.Diamond] = 0;
            //playerData.updradeLevels[(int)UpgradeType.Bonus] = 2;
            //playerData.updradeLevels[(int)UpgradeType.Clock] = 2;
            //playerData.updradeLevels[(int)UpgradeType.Timer] = 2;

            //TODO: Load data from save
        }

        private void EnableAction()
        {

        }

        private void DisableAction()
        {

        }

        #region Get / Set
        public int GetHighscore()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.Highscore))
            {
                playerData.highscore = PlayerPrefs.GetInt(PlayerPrefsKey.Highscore);
            }

            return playerData.highscore;
        }

        public void SetHighScore(int value)
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.Highscore))
            {
               PlayerPrefs.SetInt(PlayerPrefsKey.Highscore, value);
            }
        }

        public int GetCrypto()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.Crypto))
            {
                playerData.crypto = PlayerPrefs.GetInt(PlayerPrefsKey.Crypto);
            }

            return playerData.crypto;
        }

        public void SetCrypto(int value)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.Crypto, value);
        }

        //public int GetDiamond()
        //{
        //    return playerData.diamond;
        //}

        public int GetUpgradeLevel(UpgradeType type)
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.Upgrades))
            {
                string[] upgradeLevels = PlayerPrefs.GetString(PlayerPrefsKey.Upgrades).Split('/');

                for (int i = 0; i < upgradeLevels.Length; i++)
                {
                    playerData.updradeLevels[i] = int.Parse(upgradeLevels[i]);
                }
            }

            return playerData.updradeLevels[(int)type];
        }


        public void SetUpgradeLevel(UpgradeType type, int value)
        {
            string[] upgradeLevels = PlayerPrefs.GetString(PlayerPrefsKey.Upgrades).Split('/');
            string tempSave = "";

            for (int i = 0; i < upgradeLevels.Length; i++)
            {
                if (i == (int)type)
                {
                    playerData.updradeLevels[i] = value;
                }

                tempSave += (i + 1 < upgradeLevels.Length) ? $"{playerData.updradeLevels[i]}/" : $"{playerData.updradeLevels[i]}";
            }

            SetUpgradeLevel(tempSave);
        }

        private void SetUpgradeLevel(string value)
        {
            Debug.Log($"Save: {value}");
            PlayerPrefs.SetString(PlayerPrefsKey.Upgrades, value);
        }

        #endregion
    }
}

