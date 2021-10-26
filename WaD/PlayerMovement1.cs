using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngelTears
{
    public class PlayerMovement1 : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public Rigidbody2D rb;
        Vector2 movement;

        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
        }
    } 
}
//Simple placeholder movement script Sami feel free to trash or modify when ever you have your movement script ready. 
//Im not gona comment on what everything does as this will not be used anyways