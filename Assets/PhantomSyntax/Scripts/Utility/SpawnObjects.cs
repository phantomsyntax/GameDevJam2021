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
        
        
        // HandleGameOver   
        private bool bIsGameOver = false;
        
        // Start is called before the first frame update
        void Start() {
            StartCoroutine(HandleObjectSpawns());
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
                Instantiate(randomObject, new Vector3(randomSpawnPointX, 0.0f, spawnBoundary.z), randomObject.transform.rotation);
                
                yield return new WaitForSeconds(spawnDelayTimer);
            }
        }
    }
}
