using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] private bool isSpawn;
        [SerializeField] private GameObject[] spawnPrefs;

        private void Start()
        {
            InvokeRepeating(nameof(StartSpawning), 0f, 1f);
        }

        public void StartSpawning()
        {
            GameObject spawnedItem = Instantiate(spawnPrefs[0]);
            RandomSpawnPosition(spawnedItem);
        }

        public void StopSpawning()
        {

        }

        private void RandomSpawnPosition(GameObject spawnedItem)
        {
            Camera camera = Camera.main;

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float leftOffset = 100;
            float rightOffset = 100;
            float topOffset = 200;
            float bottomOffset = 100;

            // Get spawnedItem width and height
            SpriteRenderer itemSprite = spawnedItem.GetComponent<SpriteRenderer>();
            Vector3 itemBoundsMin = itemSprite.bounds.min;
            Vector3 itemBoundsMax = itemSprite.bounds.max;

            Vector3 screenMin = camera.WorldToScreenPoint(itemBoundsMin);
            Vector3 screenMax = camera.WorldToScreenPoint(itemBoundsMax);

            float itemWidthOffset = (screenMax.x - screenMin.x) / 2f;
            float itemHeightOffset = (screenMax.y - screenMin.y) / 2f;

            Debug.Log($"SizeX: {itemWidthOffset} | SizeY: {itemHeightOffset}");

            int randomId = Random.Range(0, 4);
            Vector3[] randomSet = new Vector3[4];
            randomSet[0] = new Vector3(leftOffset + itemWidthOffset, topOffset + itemHeightOffset, 0f);
            randomSet[1] = new Vector3(screenWidth - rightOffset - itemWidthOffset, topOffset + itemHeightOffset, 0f);
            randomSet[2] = new Vector3(leftOffset + itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);
            randomSet[3] = new Vector3(screenWidth - rightOffset - itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);

            float randomWidth = Random.Range(leftOffset + itemWidthOffset, screenWidth - rightOffset - itemWidthOffset);
            float randomHeight = Random.Range(topOffset + itemHeightOffset, screenHeight - bottomOffset - itemHeightOffset);

            Debug.Log($"ID: {randomId} | RandomWidth: {randomWidth} | RandomHeight: {randomHeight}");

            //Vector3 randomPosition = camera.ScreenToWorldPoint(new Vector3(randomWidth, randomHeight, 0f));

            Vector3 randomPosition = camera.ScreenToWorldPoint(randomSet[randomId]);
            randomPosition.y = randomPosition.y * -1;
            randomPosition.z = 0f;

            spawnedItem.transform.position = randomPosition;
        }
    }
}


