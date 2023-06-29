using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates
{
    public class UIUpgradeIcon : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI upgradeText;
        [SerializeField] private TextMeshProUGUI upgradeDetailText;
        [SerializeField] private TextMeshProUGUI upgradePriceText;

        [SerializeField] private Transform upgradeLevelParent;
        [SerializeField] private GameObject upgradeLevelPref;

        [Header("Price")]
        [SerializeField] private Transform priceIcon;
        [SerializeField] private Transform priceText;
        [SerializeField] private Transform maxText;

        [Header("Button")]
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Image upgradeButtonImage;

        private UpgradeButtonData data;
        private int level;

        private GameManager gameManager;
        private UIManager uiManager;
        private PlayerManager playerManager;
        private AudioManager audioManager;

        private Action OnUpgradeCallback;

        public void InitUI(UpgradeButtonData data, int level, Action callback)
        {
            gameManager = GameManager.Instance;
            playerManager = gameManager.PlayerManager;
            uiManager = gameManager.UIManager;
            audioManager = gameManager.AudioManager;

            OnUpgradeCallback = callback;

            this.data = data;
            this.level = level;

            InitUIUpgradeIcon(data.upgradeIcon, data.upgradeName, data.upgradeDetail[GetIndexLevel()], data.upgradePrice[GetIndexLevel()]);
            InitUIUpgradeLevel();
        }

        private void OnEnable()
        {
            upgradeButton.onClick.AddListener(OnClickUpgrade);
        }

        private void OnDisable()
        {
            upgradeButton.onClick.RemoveListener(OnClickUpgrade);
        }

        private void OnClickUpgrade()
        {
            if(!IsMaxLevel())
            {
                IncreaseUpgradeLevel();
                SaveData();

                UpdateUIUpgradeIcon(data.upgradeDetail[GetIndexLevel()], data.upgradePrice[GetIndexLevel()]);
                UpdateUIUpgradeLevel();
                OnUpgradeCallback?.Invoke();
                uiManager.UIHome.UpdateUI();

                audioManager.PlaySound(AudioSound.Upgrade);
            }
        }

        private void InitUIUpgradeIcon(Sprite sprite, string title, string detail, int price)
        {
            iconImage.sprite = sprite;
            upgradeText.text = title;
            upgradeDetailText.text = detail;

            if(!IsMaxLevel())
            {
                upgradePriceText.text = price.ToString();
            }
            else
            {
                priceIcon.gameObject.SetActive(false);
                priceText.gameObject.SetActive(false);
                maxText.gameObject.SetActive(true);
            }

            UpdateUpgradeButton();
        }

        private void UpdateUIUpgradeIcon(string detail, int price)
        {
            upgradeDetailText.text = detail;

            if (!IsMaxLevel())
            {
                upgradePriceText.text = price.ToString();
            }
            else
            {
                priceIcon.gameObject.SetActive(false);
                priceText.gameObject.SetActive(false);
                maxText.gameObject.SetActive(true);
            }
        }

        private void InitUIUpgradeLevel()
        {
            for(int i = 0; i < data.maxUpgradeLevel; i++)
            {
                UIUpgradeLevel uiUpgradeLevel = Instantiate(upgradeLevelPref, upgradeLevelParent).GetComponent<UIUpgradeLevel>();

                if(!IsMaxLevel())
                {
                    if (i < level)
                    {
                        uiUpgradeLevel.Activate();
                    }
                    else
                    {
                        uiUpgradeLevel.Deactivate();
                    }
                }
                else
                {
                    uiUpgradeLevel.MaxActivate();
                }
            }
        }

        private void UpdateUIUpgradeLevel()
        {
            for (int i = 0; i < data.maxUpgradeLevel; i++)
            {
                UIUpgradeLevel uiUpgradeLevel = upgradeLevelParent.GetChild(i).GetComponent<UIUpgradeLevel>();

                if (!IsMaxLevel())
                {
                    if (i < level)
                    {
                        uiUpgradeLevel.Activate();
                    }
                    else
                    {
                        uiUpgradeLevel.Deactivate();
                    }
                }
                else
                {
                    uiUpgradeLevel.MaxActivate();
                }
            }
        }

        private void IncreaseUpgradeLevel()
        {
            level = (level + 1 < data.maxUpgradeLevel) ? level + 1 : data.maxUpgradeLevel;
        }        

        public void UpdateUpgradeButton()
        {
            if(!IsMaxLevel())
            {
                if (IsAllowedUpgrade())
                {
                    EnableButton();
                }
                else
                {
                    DisableButton();
                }
            }
            else
            {
                DisableMaxButton();
            }
        }

        #region Button
        private void EnableButton()
        {
            upgradeButton.interactable = true;
            upgradeButtonImage.color = Color.yellow;
        }

        private void DisableButton()
        {
            upgradeButton.interactable = false;
            upgradeButtonImage.color = Color.gray;
        }

        private void DisableMaxButton()
        {
            upgradeButton.interactable = false;
            upgradeButtonImage.color = Color.green;
        }
        #endregion

        #region Helper
        private bool IsMaxLevel()
        {
            return level == data.maxUpgradeLevel;
        }

        private bool IsAllowedUpgrade()
        {
            return playerManager.GetCrypto() >= data.upgradePrice[GetIndexLevel()];
        }

        private int GetIndexLevel()
        {
            return IsMaxLevel() ? level - 1 : level;
        }

        private int GetLastedLevel()
        {
            return (level - 1 < 0) ? 0 : level - 1;
        }
        #endregion

        private void SaveData()
        {
            int playerCrypto = playerManager.GetCrypto() - data.upgradePrice[GetLastedLevel()];

            playerManager.SetCrypto(playerCrypto);
            playerManager.SetUpgradeLevel(data.upgradeType, level);
        }
    }
}

