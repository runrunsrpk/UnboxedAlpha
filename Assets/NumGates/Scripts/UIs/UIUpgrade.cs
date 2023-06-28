using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeButtonData[] buttonDatas;
        [SerializeField] private GameObject upgradeIconPref;
        [SerializeField] private Transform gridParent;
        [SerializeField] private Button exitButton;

        private PlayerManager playerManager;

        public void Show()
        {
            gameObject.SetActive(true);

            InitUI();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnDisable()
        {
            exitButton.onClick.RemoveListener(OnClickExit);
        }

        private void InitUI()
        {
            playerManager = GameManager.Instance.PlayerManager;

            foreach(UpgradeButtonData data in buttonDatas)
            {
                int playerUpgradeLevel = playerManager.GetUpgradeLevel(data.upgradeType);

                UIUpgradeIcon uiUpgradeIcon = Instantiate(upgradeIconPref, gridParent).GetComponent<UIUpgradeIcon>();
                uiUpgradeIcon.InitUI(data, playerUpgradeLevel);
            }
        }

        private void ClearUI()
        {
            foreach(Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }
        }

#region Button
        private void OnClickExit()
        {
            Hide();
            ClearUI();
        }
#endregion
    }
}

