using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 2)]
    public class LevelData : ScriptableObject
    {
        public float spawningSpeed;
        public float bonusSpawningSpeed;
        public int minSpawningAmount;
        public int maxSpawningAmount;
    }
}

