using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames.InteractionSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pickupable : MonoBehaviour, IPrimaryInteractable
    {
        public void PrimaryInteract()
        {

        }
    }
}