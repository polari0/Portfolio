using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polarith.AI.Move;

namespace FrogTime
{
    public class EnemySpawner : MonoBehaviour
    { 
        [SerializeField]
        List<GameObject> spawnpool;
        [SerializeField]
        List<GameObject> positions;

        public float spawnTimer = 3f;
        private float EnemyCount = 3;

        [SerializeField]
        AIMEnvironment AIM_Script;
        [SerializeField]
        PointCounter pointCounter_Script;

        internal int roundCount = 0;
        [SerializeField]
        int maxRounds = 10;

        private void Awake()
        {
            StartCoroutine(StartSpawning());
        }

        private void Update()
        {
            if (roundCount == maxRounds + 1)
            {
                Debug.Log(roundCount);
                StopCoroutine(StartSpawning());
            }

        }

        internal void SpawnFromLocation()
        {
            int randomEnemy;
            GameObject EnemyToSpawn;
            int SpawnPos = Random.Range(0, positions.Count);
            //Chooses random postion from the 4 available positions. 

            randomEnemy = Random.Range(0, spawnpool.Count);
            EnemyToSpawn = spawnpool[randomEnemy];
            //This is here for possibility to extend this code later on to have multiple different enemy types as I originally envisioned this to be but for now it is useless

            Instantiate(EnemyToSpawn, positions[SpawnPos].transform.position, transform.rotation);
            //This function spawn the enemies to the chosen positions.
        }
        private IEnumerator StartSpawning()
        {
            while (roundCount <= maxRounds)
            {
                for (int i = 0; i < EnemyCount; i++)
                {
                    SpawnFromLocation();
                    AIM_Script.UpdateLayerGameObjects();
                    pointCounter_Script.UpdateRoundCount();
                    yield return new WaitForSeconds(spawnTimer); 
                }
                spawnTimer -= 0.2f;
                roundCount++;
                EnemyCount++; 
                yield return new WaitForSeconds(5f);
            }
        }
    } 
}
