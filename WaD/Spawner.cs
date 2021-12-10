using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class Spawner : MonoBehaviour
    {
        public int numberToSpawn;

        public List<GameObject> spawnpool;
        [SerializeField]
        List<GameObject> Angels;
        //These lists store what to spawn and position to spawn. 
        public GameObject quad;
        public int collectTears = 0;
        public void StartGameTears()
        {
            StartCoroutine(TearDrop());
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
            //This script spawns opbjects on the quad on top 
        }

        internal void SpawFromAngels()
        {
            int tear;
            GameObject tearToSapwn;
            int angelPos = Random.Range(0, Angels.Count);
            //Chooses random postion from the 4 available angel positions. 

            tear = Random.Range(0, spawnpool.Count);
            tearToSapwn = spawnpool[tear];
            //These 2 line are here just because i made a the original code before thinking this far ahead and im too lazy to change it.
            //But basically they just take random item from spawnpool which has only one item. 

            Instantiate(tearToSapwn, Angels[angelPos].transform.position, transform.rotation);
            //This function spawn the tears at Angel positions. Angel positions are stored in list and i take random index of that list to choose the position to spawn the tear. 
        }

        IEnumerator TearDrop()
        {
            while (collectTears < 10)
            {
                yield return new WaitForSeconds(3);
                //SpanwObjects();
                SpawFromAngels();
            }
        }
    }
}