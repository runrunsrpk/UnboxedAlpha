using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public interface ICollectable
    {
        public void Init();
        public void OnCollected();
    }
}

