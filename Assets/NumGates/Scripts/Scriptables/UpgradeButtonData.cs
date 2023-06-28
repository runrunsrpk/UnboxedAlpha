using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    [CreateAssetMenu(fileName = "UpgradeButtonData", menuName = "ScriptableObjects/UpgradeButtonData", order = 0)]
    public class UpgradeButtonData : ScriptableObject
    {
        public Sprite upgradeIcon;
        public string upgradeName;
        public int maxUpgradeLevel;
        public string[] upgradeDetail;
        public int[] upgradePrice;
    }
}

