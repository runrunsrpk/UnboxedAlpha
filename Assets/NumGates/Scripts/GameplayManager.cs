using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class GameplayManager : MonoBehaviour
    {
        public Action<int> OnSoulCollected;

        private int score;
        private int crpto;
        private int diamond;
        private int unboxed;
        private int health;

        private float timer;
        private float unboxedTimer;
        private float shieldTimer;

        private void Start()
        {
            OnSoulCollected += SoulColledted;
        }

        public void SoulColledted(int value)
        {
            score += value;
        }
    }
}

