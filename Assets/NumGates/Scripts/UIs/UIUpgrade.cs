using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIUpgrade : MonoBehaviour
    {
        [SerializeField] private Button exitButton;

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

        }

        #region Button
        private void OnClickExit()
        {
            Hide();
        }
        #endregion
    }
}

