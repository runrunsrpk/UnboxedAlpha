using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class Crypto : Collectable
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
            gameplayManager.OnCryptoCollected?.Invoke(value);

            Destroy(gameObject);
        }
    }
}

