using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class TearCollection : MonoBehaviour
    {
        [SerializeField]
        private Spawner spawner_Script;
        private void OnTriggerEnter2D(Collider2D collision)
        {
             if (collision.gameObject.tag == "Spawnable" && spawner_Script.collectTears < 10)
             {
                spawner_Script.collectTears += 1;
                Destroy(collision.gameObject);
                print(spawner_Script.collectTears);
             } 
        }
    } 
}
