using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UISymbol : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void Init()
        {
            image.color = Color.yellow;
        }

        public void Activate()
        {
            image.color = Color.green;
        }

        public void Deactivate()
        {
            image.color = Color.white;
        }
    }
}

