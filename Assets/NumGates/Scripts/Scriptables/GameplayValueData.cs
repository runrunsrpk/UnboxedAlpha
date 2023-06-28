using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    [CreateAssetMenu(fileName = "GameplayValueData", menuName = "ScriptableObjects/GameplayValueData", order = 1)]
    public class GameplayValueData : ScriptableObject
    {
        public int[] healthValue;
        public float[] shieldTimerValue;
        public int[] pureSoulValue;
        public int[] colorSoulValue;
        public int[] cryptoValue;
        public int[] diamondValue;
        public float[] bonusTimerValue;
        public int[] clockValue;
        public float[] gameTimerValue;
    }
}

