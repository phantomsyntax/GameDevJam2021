using System.Collections;
using System.Collections.Generic;
using PhantomSyntax.Scripts.Interfaces;
using PhantomSyntax.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PhantomSyntax.Scripts.Utility {
    public class SpawnObjects : MonoBehaviour, /*ICheckpointObserver,*/ ILevelObserver {
        [Header("Object Spawn Settings")]
        [SerializeField] private List<GameObject> spawnableObjects;
        [SerializeField] private float spawnDelayTimer = 3.0f;
        [SerializeField] private int maximumObjectsOnScreen = 2;
        [SerializeField] private Vector3 spawnBoundary = new Vector3(4.0f, 0.0f, 35.0f);

        [Header("Checkpoint Spawn Settings")]
        [SerializeField] private GameObject checkpointPrefab;
        [SerializeField] private int checkpointsNeededToWin = 3;
        [SerializeField] private IntegerValue checkpointsPassed;
        public float checkpointDelayTimer = 10.0f;

        [Header("Level Completion Settings")]
        [SerializeField] private GameObject playerFollowPoint;
        [SerializeField] private UserInterfaceManager userInterfaceManager;
        [SerializeField] private GameObject player;
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

            if (!playerFollowPoint) {
                playerFollowPoint = GameObject.FindWithTag("PlayerFollowPoint");
            }

            if (!player) {
                player = GameObject.FindWithTag("Player");
            }
            
            // Null checks for tokensText and checkpointIndicators(?)
        }

        // Update is called once per frame
        void Update()
        {
            // Player gets all checkpoints 
            if (checkpointsPassed.Value == checkpointsNeededToWin && !bIsGameOver) {
                HandleGameWinLose(bPlayerHasWon);
                TriggerCameraRotation();
            }
            // bPlayerHasWon is set by Obstacle collisions
            if (!bPlayerHasWon && !bIsGameOver) {
                HandleGameWinLose(bPlayerHasWon);
            }
        }

        void HandleGameWinLose(bool bPlayerHasWon) {
                StopObjectSpawning();
                DestroyActiveObjects();
                UpdateWinLoseUI();
                
                // Switches current InputSystem map to disable player movement
                player.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
                
                bIsGameOver = true;
                checkpointsPassed.Value = 0;
        }
        
        public void StopObjectSpawning() {
            StopCoroutine(HandleObjectSpawns());
            StopCoroutine(HandleCheckpointSpawns());
            bIsGameOver = true;
        }
        
        public void UpdateWinLoseUI() {
            // Gets moved to GameManager loop?
            userInterfaceManager.ToggleWinLoseText(bPlayerHasWon);
        }

        public void TriggerCameraRotation() { 
            // Rotate the player's FollowPoint to get the CM to chase it
            // TODO: slerp the rotation so it slows down
            playerFollowPoint.transform.Rotate(new Vector3(-26.0f, 160.0f, 0.0f));
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