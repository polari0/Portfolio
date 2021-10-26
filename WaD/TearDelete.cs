using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class TearDelete : MonoBehaviour
    {
        public GameObject tear;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Spawnable")
            {
                Destroy(collision.gameObject);
            }
        }
    } 
}
