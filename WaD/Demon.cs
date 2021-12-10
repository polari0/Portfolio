using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class Demon : MonoBehaviour
    {
        [SerializeField]
        private Spawner spawner_Script;
        public GameObject demon;
        public GameObject Player;
        [SerializeField]
        GameObject pitchFork;
        //References for gameobjects


        private float speed = 3.5f;
        private float distance;
        private Transform target;
        //Variables for the script

        internal bool hasGameStarted = false;
        private bool haveWePitchForked = false;

        //private int tearsCollectedDemon = 0;

        //Doing it the dum fuck way 
        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        internal void Update()
        {
            var targetPos = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            //Movement calculations for the demon.
            distance = (Player.transform.position.x - demon.transform.position.x);
            //Distance calculation between player and demon for pitchfork swing

            if (Mathf.Abs(distance) < 0.1f && hasGameStarted == true && haveWePitchForked == false)
            {
                StartCoroutine(StartPitchFork());
            }

        }
        //Moves the demon bit behind the player character. 
        internal void PitchFork()
        {
            if (spawner_Script.collectTears < 10)
            {
                Instantiate(pitchFork, d.transform.position, transform.rotation);
            }
            else if (spawner_Script.collectTears == 10)
            {
                hasGameStarted = false;
            }
            //spawns new pitchfork
        }
        internal IEnumerator StartPitchFork()
        {
            haveWePitchForked = true;
            yield return new WaitForSeconds(1);
            PitchFork();
            haveWePitchForked = false;
        }
    } 
}
