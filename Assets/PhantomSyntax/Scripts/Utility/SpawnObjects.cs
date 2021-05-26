using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class SpawnObjects : MonoBehaviour {
        [Header("Object Spawn Settings")]
        [SerializeField] private List<GameObject> spawnableObjects;
        [SerializeField] private float spawnDelayTimer = 3.0f;
        [SerializeField] private int maximumObjectsOnScreen = 2;
        [SerializeField] private Vector3 spawnBoundary = new Vector3(4.0f, 0.0f, 35.0f);

        [Header("Checkpoint Spawn Settings")]
        [SerializeField] private GameObject checkpointPrefab;
        [SerializeField] private float checkpointDelayTimer = 10.0f;
        
        // HandleGameOver   
        private bool bIsGameOver = false;
        
        // Start is called before the first frame update
        void Start() {
            if (spawnableObjects.Count > 0) {
                StartCoroutine(HandleObjectSpawns());   
            }

            if (checkpointPrefab) {
                StartCoroutine(HandleCheckpoingSpawns());
            }
        }

        // Update is called once per frame
        void Update()
        {
        
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

        IEnumerator HandleCheckpoingSpawns() {
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
