using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class SceneStart : MonoBehaviour
    {
        //    private void OnTriggerEnter2D(Collider2D collision)
        //    {
        //        print("Yes");
        //    }
        //    private void OnTriggerStay2D(Collider2D collision)
        //    {
        //        if (Input.GetKeyDown("e"))
        //        {
        //            print("Yes");
        //        }
        //    }
        //}
        //Mostly my failed aptempts while i tested all this out
        [SerializeField]
        private Spawner spawner_Script;
        private bool hasPlayerTouched = false;
        [SerializeField]
        private Demon demon_Script;
        //Serialized field is needed for to call Gamestart function when you press e to start the game. 
        //Bool checks wether or not you have touched the tv so you cant start the scene from other side of the map


        //private void Start()
        //{
        //    gameStart = GetComponent<Spawner>();
        //}
        //This was somethin i tried it did not work out but lets leave it here for now


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                hasPlayerTouched = true;
            }
        }
        //Simple touch check to see if you have touched the tv
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                hasPlayerTouched = false;
            }
        }
        private void Update()
        {
            if(hasPlayerTouched == true && Input.GetKeyDown("e"))
            {
                print("e is pressed");
                spawner_Script.StartGameTears();
                //StartCoroutine(demon_Script.StartPitchFork());
                demon_Script.hasGameStarted = true;
            }
        }
        //Once you have touched the TV pressing e will start the scene this piece of code will handle that
    }
}
//Sami get me some place holder sprites. 

