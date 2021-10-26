using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class Demon : MonoBehaviour
    {
        public GameObject Player;
        public GameObject demon;
        private float speed = 3.5f;
        private float distance;
        private Transform target;
        //private float playerPosY;
        //private float playerPosX;
        //private float demonPosX;
        //private float demonPosY;
        //Setting some variables for the script 
        //demonPos and PlayerPos are for stuff that might or might not work
        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            //Setting game object variable target is attached to. 
            GameObject spawner = GameObject.Find("Spawner");
            Spawner _spawner = spawner.GetComponent<Spawner>();
            //Code is wrong but I have worked long enough for today to fix it now
            //https://answers.unity.com/questions/42843/referencing-non-static-variables-from-another-scri.html check that site later when you fix this 
        }

        internal void Update()
        {
            var targetPos = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            distance = (Player.transform.position.x - demon.transform.position.x);
        }
        //Moves the demon bit behind the player character. 
        internal void PitchFork()
        {
            //playerPosX = Player.transform.position.x;
            //playerPosY = Player.transform.position.y;
            //demonPosX = demon.transform.position.x;
            //demonPosY = demon.transform.position.y;

            while (tearsColledted.collectTears < 10)
            {
                if(distance > 0.01f)
                {
                    print("Im coming");
                }
            }

        }
    } 
}
