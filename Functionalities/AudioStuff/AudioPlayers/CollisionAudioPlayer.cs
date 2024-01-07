using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HexKeyGames
{
    public class CollisionAudioPlayer : AudioPlayer
    {
        [SerializeField]
        private AudioBankObj softHit, hardHit;

        [SerializeField]
        private float softHitThreshold = 1, hardHitThreshold = 5;

        private void OnCollisionEnter(Collision collision)
        {
            audioSource.volume = CalculateImpactVolume(collision.relativeVelocity.magnitude);
            if (collision.relativeVelocity.magnitude > hardHitThreshold)
            {
                audioBankObj = hardHit;
                PlayRandomAudioClip();
            }
            else if (collision.relativeVelocity.magnitude > softHitThreshold)
            {
                audioBankObj = softHit;
                PlayRandomAudioClip();
            }
            else
                return;
        }

        private float CalculateImpactVolume(float impactMagnitude)
        {
            float impactVolume = Mathf.Clamp(impactMagnitude, 0f, 10f)/10 + 0.1f;
            return impactMagnitude;
        }
    }
}
