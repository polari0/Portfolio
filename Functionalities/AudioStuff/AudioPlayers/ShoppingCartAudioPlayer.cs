using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class ShoppingCartAudioPlayer : AudioPlayer
    {
        public void ShoppingCartSounds()
        {
            if (audioSource.isPlaying)
            {
                return;
            }
            else
            {
                PlayRandomAudioClip();
            }
        }
    }
}
