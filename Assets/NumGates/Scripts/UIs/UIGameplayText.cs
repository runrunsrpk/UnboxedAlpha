using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NumGates
{
    public class UIGameplayText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;

        public void InitUI()
        {
            UpdateText(0);
        }

        public void UpdateText(int value)
        {
            textMesh.text = value.ToString();
        }

        public void UpdateTextValue(int value)
        {
            int currentValue = int.Parse(textMesh.text);

            textMesh.text = (currentValue + value).ToString();
        }

        public int GetTextValue()
        {
            return int.Parse(textMesh.text);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

