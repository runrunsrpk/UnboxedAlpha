using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIGameplay : MonoBehaviour
    {
        [Header("UI Outer Frames")]
        [SerializeField] private Transform leftFrame;
        public Transform LeftFrame => leftFrame;

        [SerializeField] private Transform rightFrame;
        public Transform RightFrame => rightFrame;

        [SerializeField] private Transform topFrame;
        public Transform TopFrame => topFrame;

        [SerializeField] private Transform bottomFrame;
        public Transform BottomFrame => bottomFrame;

        [Header("UI Inner Frames")]
        [SerializeField] private Transform contentTopFrame;
        public Transform ContentTopFrame => contentTopFrame;

        [SerializeField] private Transform contentBottomFrame;
        public Transform ContentBottomFrame => contentBottomFrame;

        [Header("UI Components")]
        [SerializeField] private GameObject uiScore;
        [SerializeField] private GameObject uiUnboxedSymbols;
        [SerializeField] private GameObject uiUnboxedGauge;
        [SerializeField] private GameObject uiDiamond;
        [SerializeField] private GameObject uiCrypto;

        [Header("UI Button")]
        [SerializeField] private Button pauseButton;
    }
}

