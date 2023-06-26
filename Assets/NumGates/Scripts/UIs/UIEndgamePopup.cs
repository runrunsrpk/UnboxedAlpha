using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class UIEndgamePopup : MonoBehaviour
    {
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

