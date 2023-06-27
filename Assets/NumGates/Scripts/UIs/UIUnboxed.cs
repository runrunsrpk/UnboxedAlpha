using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIUnboxed : MonoBehaviour
    {
        [SerializeField] private Transform unboxedGroup;
        [SerializeField] private Transform unboxedGauge;

        private Slider bonusSlider;

        private int maxSymbol = 7;
        private int symbolAmount;

        private GameplayManager gameplayManager;

        public void InitUI()
        {
            gameplayManager = GameManager.Instance.GameplayManager;

            bonusSlider = unboxedGauge.GetComponent<Slider>();

            DisableBonus();
        }

        public void AddSymbol()
        {
            if(symbolAmount + 1 < maxSymbol)
            {
                UISymbol uiSymbol = unboxedGroup.GetChild(symbolAmount).GetComponent<UISymbol>();
                uiSymbol.Init();
                symbolAmount++;
            }
            else
            {
                gameplayManager.OnStartBonusTimer?.Invoke();
                symbolAmount = 0;
            }
        }

        public void EnableBonus()
        {
            unboxedGauge.gameObject.SetActive(true);

            foreach(Transform child in unboxedGroup)
            {
                UISymbol uiSymbol = child.GetComponent<UISymbol>();
                uiSymbol.Activate();
            }
        }

        public void UpdateBonus(float value, float maxValue)
        {
            bonusSlider.value = value / maxValue;
        }

        public void DisableBonus()
        {
            unboxedGauge.gameObject.SetActive(false);

            foreach (Transform child in unboxedGroup)
            {
                UISymbol uiSymbol = child.GetComponent<UISymbol>();
                uiSymbol.Deactivate();
            }
        }
    }
}

