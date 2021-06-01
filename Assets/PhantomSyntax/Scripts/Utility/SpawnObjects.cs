using System.Collections;
using System.Collections.Generic;
using PhantomSyntax.Scripts.ScriptableObjects;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class SpawnObjects : MonoBehaviour {
        [Header("Object Spawn Settings")]
        [SerializeField] private List<GameObject> spawnableObjects;
        [SerializeField] private int maximumObjectsOnScreen = 2;
        [SerializeField] private Vector3 spawnBoundary = new Vector3(4.0f, 0.0f, 35.0f);
        [SerializeField] private float spawnObjectDelay = 3.0f;
        
        [Header("Checkpoint Spawn Settings")]
        [SerializeField] private GameObject checkpointPrefab;
        [SerializeField] private int checkpointsNeededToWin = 3;
        [SerializeField] private IntegerValue checkpointsPassed;
        [SerializeField] private float spawnCheckpointDelay = 10.0f;

        [Header("Level Completion Settings")]
        [SerializeField] private GameEvent levelComplete;
        public bool bPlayerHasWon = true;
        
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

            // Null checks for tokensText and checkpointIndicators(?)
        }

        // Update is called once per frame
        void Update()
        {
            // Player gets all checkpoints 
            if (checkpointsPassed.Value == checkpointsNeededToWin && !bIsGameOver) {
                HandleGameWinLose(bPlayerHasWon);
            }
            // bPlayerHasWon is set by Obstacle collisions
            if (!bPlayerHasWon && !bIsGameOver) {
                HandleGameWinLose(bPlayerHasWon);
            }
        }

        void HandleGameWinLose(bool bPlayerHasWon) {
                StopObjectSpawning();
                DestroyActiveObjects();
                
                // Triggers camera rotation and update for Win/Lose UI
                levelComplete.Triggered();
                levelComplete.ConditionalBool(bPlayerHasWon);
                
                bIsGameOver = true;
                checkpointsPassed.Value = 0;
        }
        
        public void StopObjectSpawning() {
            StopCoroutine(HandleObjectSpawns());
            StopCoroutine(HandleCheckpointSpawns());
            bIsGameOver = true;
        }

        void DestroyActiveObjects() {
            var activeObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in activeObstacles) {
                Destroy(obstacle);
            }

            var activeCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            foreach (GameObject checkpoint in activeCheckpoints) {
                Destroy(checkpoint);
            }
        }

        IEnumerator HandleObjectSpawns() {
            while (!bIsGameOver) {
                // Pull a random object from spawnableObjects and Instantiate it based on the supplied boundary values
                GameObject randomObject = spawnableObjects[Random.Range(0, spawnableObjects.Count)];
                float randomSpawnPointX = Random.Range(-spawnBoundary.x, spawnBoundary.x);
                Instantiate(randomObject, new Vector3(randomSpawnPointX, randomObject.transform.position.y, spawnBoundary.z), randomObject.transform.rotation);

                yield return new WaitForSeconds(spawnObjectDelay);
            }
        }

        IEnumerator HandleCheckpointSpawns() {
            yield return new WaitForSeconds(spawnCheckpointDelay);
            
            while (!bIsGameOver) {
                // Spawn a checkpoint based on a selected timer
                Vector3 checkpointSpawnPosition = new Vector3(checkpointPrefab.transform.position.x, checkpointPrefab.transform.position.y, spawnBoundary.z);
                Instantiate(checkpointPrefab, checkpointSpawnPosition, checkpointPrefab.transform.rotation);
                yield return new WaitForSeconds(spawnCheckpointDelay);
            }
        }
    }
}