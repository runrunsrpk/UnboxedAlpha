using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class PureSoul : Collectable
    {
        protected override void Init()
        {
            base.Init();

        }

        protected override void Destroy()
        {

        }

        protected override void OnCollected()
        {
            gameplayManager.OnSoulCollected?.Invoke(value);

            Destroy(gameObject);
        }
    }
}


