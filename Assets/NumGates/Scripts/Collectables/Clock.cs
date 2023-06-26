using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class Clock : Collectable
    {
        protected override void Init()
        {
            base.Init();

        }

        protected override void Destroy()
        {
            base.Destroy();
        }

        protected override void Collected()
        {
            gameplayManager.OnClockCollected?.Invoke(value);

            Destroy(gameObject);
        }
    }
}

