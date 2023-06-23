using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class SpawnerManager : MonoBehaviour
    {
        [Header("Customize")]
        [SerializeField] private bool isSpawn;
        [SerializeField] private float frameOffset;
        [SerializeField] private GameObject[] spawnPrefs;

        [Header("UIs")]
        [SerializeField] private UIGameplay uiGameplay;

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

            Debug.Log($"ScreenWidth: {screenWidth} | ScreenHeight: {screenHeight}");

            // Calculate frame size
            Vector2 leftFrameSize = UnboxedUtils.CalculateUISize(uiGameplay.LeftFrame, frameOffset);
            Vector2 rightFrameSize = UnboxedUtils.CalculateUISize(uiGameplay.RightFrame, frameOffset);
            Vector2 topFrameSize = UnboxedUtils.CalculateUISize(uiGameplay.TopFrame, frameOffset);
            Vector2 bottomFrameSize = UnboxedUtils.CalculateUISize(uiGameplay.BottomFrame, frameOffset);

            Vector2 contentTopFrameSize = UnboxedUtils.CalculateUISize(uiGameplay.ContentTopFrame, frameOffset);
            Vector2 contentBottomFrameSize = UnboxedUtils.CalculateUISize(uiGameplay.ContentBottomFrame, frameOffset);

            // Create frame offset
            float leftOffset = leftFrameSize.x;
            float rightOffset = rightFrameSize.x;
            float topOffset = topFrameSize.y + contentTopFrameSize.y;
            float bottomOffset = bottomFrameSize.y + contentBottomFrameSize.y;

            Debug.Log($"Offset L: {leftOffset} | Offset R: {rightOffset} | Offset T: {topOffset} | Offset B: {bottomOffset}");

            // Calculate spawned item size
            Vector2 spawnedSize = UnboxedUtils.CalculateSpriteSize(spawnedItem);

            // Create spawned item offset
            float itemWidthOffset = spawnedSize.x / 2f;
            float itemHeightOffset = spawnedSize.y / 2f;

            Debug.Log($"SizeX: {itemWidthOffset} | SizeY: {itemHeightOffset}");

            //TODO: Create offset space

            int randomId = Random.Range(0, 4);
            Vector3[] randomSet = new Vector3[4];
            randomSet[0] = new Vector3(leftOffset + itemWidthOffset, topOffset + itemHeightOffset, 0f);
            randomSet[1] = new Vector3(screenWidth - rightOffset - itemWidthOffset, topOffset + itemHeightOffset, 0f);
            randomSet[2] = new Vector3(leftOffset + itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);
            randomSet[3] = new Vector3(screenWidth - rightOffset - itemWidthOffset, screenHeight - bottomOffset - itemHeightOffset, 0f);

            //float randomWidth = Random.Range(leftOffset + itemWidthOffset, screenWidth - rightOffset - itemWidthOffset);
            //float randomHeight = Random.Range(topOffset + itemHeightOffset, screenHeight - bottomOffset - itemHeightOffset);

            //Debug.Log($"ID: {randomId} | RandomWidth: {randomWidth} | RandomHeight: {randomHeight}");

            //Vector3 randomPosition = camera.ScreenToWorldPoint(new Vector3(randomWidth, randomHeight, 0f));

            Vector3 randomPosition = camera.ScreenToWorldPoint(randomSet[randomId]);
            randomPosition.y = randomPosition.y * -1;
            randomPosition.z = 0f;

            spawnedItem.transform.position = randomPosition;
        }
    }
}


