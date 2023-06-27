using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class Collectable : MonoBehaviour
    {
        public Action OnCollected;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider boxCollider;

        [SerializeField] protected int value;
        [SerializeField] protected bool hasLifeTime;
        [SerializeField] protected bool hasDamage;
        [SerializeField] protected float lifeTime;

        protected GameManager gameManager;
        protected GameplayManager gameplayManager;

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void Update()
        {
            UpdateLiftTime();
        }

        protected virtual void OnDestroy()
        {
            Destroy();
        }

        protected virtual void Init()
        {
            gameManager = GameManager.Instance;
            gameplayManager = gameManager.GameplayManager;

            OnCollected += Collected;
        }

        private void UpdateLiftTime()
        {
            if (hasLifeTime == false) return;

            if (gameplayManager.IsStart == false) return;

            if (gameplayManager.IsPause == true) return;

            if (lifeTime > 0f)
            {
                lifeTime -= Time.deltaTime;
            }
            else
            {
                lifeTime = 0f;
                if(hasDamage == true && gameplayManager.IsShield == false)
                {
                    gameplayManager.OnSoulMissed?.Invoke();
                }
                Destroy(gameObject);
            }
        }

        protected virtual void Destroy()
        {
            OnCollected -= Collected;
        }

        protected virtual void Collected()
        {

        }
    }
}

