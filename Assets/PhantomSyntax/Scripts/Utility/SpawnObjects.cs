using System.Collections;
using System.Collections.Generic;
using PhantomSyntax.Scripts.Interfaces;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class SpawnObjects : MonoBehaviour, ICheckpointObserver, ILevelObserver {
        [Header("Object Spawn Settings")]
        [SerializeField] private List<GameObject> spawnableObjects;
        [SerializeField] private float spawnDelayTimer = 3.0f;
        [SerializeField] private int maximumObjectsOnScreen = 2;
        [SerializeField] private Vector3 spawnBoundary = new Vector3(4.0f, 0.0f, 35.0f);

        [Header("Checkpoint Spawn Settings")]
        [SerializeField] private GameObject checkpointPrefab;
        [SerializeField] private int checkpointsNeededToWin = 3;
        public float checkpointDelayTimer = 10.0f;

        [Header("Level Completion Settings")]
        [SerializeField] private GameObject playerFollowPoint;
        
        // ICheckpointObserver
        public int CheckpointsNeeded {
            get { return checkpointsNeededToWin; }
            set { checkpointsNeededToWin = value; }
        }
        
        // HandleGameOver   
        private bool bIsGameOver = false;
        
        // Start is called before the first frame update
        void Start() {
            if (spawnableObjects.Count > 0) {
                StartCoroutine(HandleObjectSpawns());   
            }

            if (checkpointPrefab) {
                StartCoroutine(HandleCheckpointSpawns());
            }

            if (!playerFollowPoint) {
                playerFollowPoint = GameObject.FindWithTag("PlayerFollowPoint");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (CheckpointsNeeded < 1 && !bIsGameOver) {
                StopObjectSpawning();
                DestroyActiveObjects();
                UpdateWinLoseUI();
                TriggerCameraRotation();
                
                bIsGameOver = true;
            }
        }
        
        public void UpdateCheckpointUI() {
            print("--- Add a flag to the checkpoint UI");
            CheckpointsNeeded--;
        }

        public void StopObjectSpawning() {
            print("--- Stop the spawning of objects");
            StopCoroutine(HandleObjectSpawns());
            StopCoroutine(HandleCheckpointSpawns());
            bIsGameOver = true;
        }
        public void UpdateWinLoseUI() {
            print("--- Change Win/Lose UI Text and appearance");
        }

        public void TriggerCameraRotation() {
            print("--- Rotate the camera around to face the player");
            // Rotate the player's FollowPoint to get the CM to chase it
            playerFollowPoint.transform.Rotate(new Vector3(-26.0f, 160.0f, 0.0f));
            float followPointZ = playerFollowPoint.transform.position.z;
            followPointZ = 0.0f;
        }
        
        void DestroyActiveObjects() {
            print("--- Destroying all active objects");
            var activeObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in activeObstacles) {
                Destroy(obstacle);
            }
        }

        IEnumerator HandleObjectSpawns() {
            while (!bIsGameOver) {
                // Pull a random object from spawnableObjects and Instantiate it based on the supplied boundary values
                GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Count)];
                float randomSpawnPointX = Random.Range(-spawnBoundary.x, spawnBoundary.x);
                Instantiate(randomObject, new Vector3(randomSpawnPointX, randomObject.transform.position.y, spawnBoundary.z), randomObject.transform.rotation);
                
                yield return new WaitForSeconds(spawnDelayTimer);
            }
        }

        IEnumerator HandleCheckpointSpawns() {
            int totalSpawnedCheckpoints = 0;
            yield return new WaitForSeconds(checkpointDelayTimer);
            
            while (!bIsGameOver) {
                // Spawn a checkpoint based on a selected timer
                Vector3 checkpointSpawnPosition = new Vector3(checkpointPrefab.transform.position.x, checkpointPrefab.transform.position.y, spawnBoundary.z);
                Instantiate(checkpointPrefab, checkpointSpawnPosition, checkpointPrefab.transform.rotation);
                totalSpawnedCheckpoints++; // Adding in for gameloop completion
                yield return new WaitForSeconds(checkpointDelayTimer);
            }
        }

    }
}
