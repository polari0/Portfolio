using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class PitchFork : MonoBehaviour
    {
        float moveSpeed = 1f;
        private bool canWeMove = false;

        private void OnEnable()
        {
            Destroy(gameObject, 3f);
            Invoke("AllowMeToMove", 0.2f);
        }

        void Update()
        {
            if (canWeMove)
            {
                Vector3 newPosition = transform.position;
                transform.position = transform.position + new Vector3(0, 2 * moveSpeed * Time.deltaTime, 0); 
            }
        }

        private void AllowMeToMove()
        {
            canWeMove = true;
        }
    }
}