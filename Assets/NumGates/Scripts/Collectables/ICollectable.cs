using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public interface ICollectable
    {
        public Action OnCollected { get; set; }
    }
}

