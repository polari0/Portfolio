using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class CheckOutAudio : AudioPlayer
    {
        [SerializeField]
        private GameObject welcomeScreen;           // Audio will not be played when this object is disabled (if assigned).
        private bool thisCheckoutCooldown;          // Audio will not be played when timer is active.
        private static bool allCheckoutsCooldown;   // Audio will not be played when timer is active. Shared by all checkouts.

        private void OnTriggerStay(Collider other)
        {
            if (thisCheckoutCooldown || allCheckoutsCooldown) return;               // Audio was played recently -> do nothing.
            if (!other.TryGetComponent<Character>(out _)) return;                   // Collider is not player -> do nothing.                           
            if (welcomeScreen != null && !welcomeScreen.activeInHierarchy) return;  // Checkout is not in the "welcome" state -> do nothing.

            PlayRandomAudioClip();
            StartCoroutine(AllCheckoutsTimer());
            StartCoroutine(ThisCheckoutTimer());
        }

        private IEnumerator ThisCheckoutTimer()
        {
            thisCheckoutCooldown = true;
            yield return new WaitForSeconds(8f);
            thisCheckoutCooldown = false;
        }

        private IEnumerator AllCheckoutsTimer()
        {
            allCheckoutsCooldown = true;
            yield return new WaitForSeconds(3.5f);
            allCheckoutsCooldown = false;
        }
    }
}
