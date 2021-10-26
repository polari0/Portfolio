using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class Spawner : MonoBehaviour
    {
        public int numberToSpawn;
        public List<GameObject> spawnpool;
        public GameObject quad;
        public int collectTears = 0;
        public void StartGameTears()
        {
            StartCoroutine(tearDrop());
        }
        public void SpanwObjects()
        {
            int randomItem = 0;
            GameObject toSpawn;
            MeshCollider c = quad.GetComponent<MeshCollider>();

            float screenX, screenY;
            Vector2 pos;

            randomItem = Random.Range(0, spawnpool.Count);
            toSpawn = spawnpool[randomItem];

            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.min.y);
            //Setting boundaries where tears can spawn
            pos = new Vector2(screenX, screenY);
            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            //Instantiate spawns the tears

        }
        IEnumerator tearDrop()
        {
            while (collectTears < 10)
            {
                yield return new WaitForSeconds(3);
                SpanwObjects();
            }
        }
    }
}