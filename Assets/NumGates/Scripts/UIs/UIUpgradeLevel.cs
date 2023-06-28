using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIUpgradeLevel : MonoBehaviour
    {
        [SerializeField] private Image levelImage;

        public void Activate()
        {
            levelImage.color = Color.yellow;
        }

        public void Deactivate()
        {
            levelImage.color = Color.black;
        }

        public void MaxActivate()
        {
            levelImage.color = Color.green;
        }
    }
}

