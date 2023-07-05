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
        protected AudioManager audioManager;

        public void InitValue(int value)
        {
            this.value = value;
        }

        protected virtual void Start()
        {
            Init();

            audioManager.PlaySound(AudioSound.ItemSpawn);
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
            audioManager = gameManager.AudioManager;

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
                else
                {
                    audioManager.PlaySound(AudioSound.ItemDestroy);
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

