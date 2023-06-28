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

        private UpgradeButtonData data;
        private int level;

        public void InitUI(UpgradeButtonData data, int level)
        {
            this.data = data;
            this.level = level;

            InitUIUpgradeIcon(data.upgradeIcon, data.upgradeName, data.upgradeDetail[level], data.upgradePrice[level]);
            InitUIUpgradeLevel();
        }

        private void OnClickUpgrade()
        {
            if(!IsMaxLevel())
            {

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

        private void UpdateUpgradeButton()
        {

        }

        private bool IsMaxLevel()
        {
            return level == data.maxUpgradeLevel;
        }
    }
}

