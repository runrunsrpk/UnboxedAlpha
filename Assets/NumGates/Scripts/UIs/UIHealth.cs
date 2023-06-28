using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private Transform healthGroup;
        [SerializeField] private Transform shieldGroup;

        private int health;
        private int maxHealth;

        private GameManager gameManager;
        private GameplayManager gameplayManager;

        public void InitUI()
        {
            gameManager = GameManager.Instance;
            gameplayManager = gameManager.GameplayManager;

            maxHealth = gameplayManager.GameplayData.health;
            health = gameplayManager.GameplayData.health;

            foreach(Transform child in healthGroup)
            {
                child.gameObject.SetActive(false);
            }

            for(int i = 0; i < health; i++)
            {
                GameObject child = healthGroup.GetChild(i).gameObject;
                child.SetActive(true);
                child.GetComponent<UIHealthIcon>().Init();
            }

            DisableShield();
        }

        public void Damaged()
        {
            int index = maxHealth - health;
            UIHealthIcon healthIcon = healthGroup.GetChild(index).GetComponent<UIHealthIcon>();
            healthIcon.Damaged();

            if (health - 1 > 0)
            {
                health--;
            }
            else
            {
                gameplayManager.OnEndGame?.Invoke();
            }
        }

        public void EnableShield()
        {
            shieldGroup.gameObject.SetActive(true);
        }

        public void DisableShield()
        {
            shieldGroup.gameObject.SetActive(false);
        }
    }
}

