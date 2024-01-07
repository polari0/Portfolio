using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private protected AudioSource audioSource;
        [SerializeField]
        private protected AudioBankObj audioBankObj;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// These functions are used to play sounds from the sound bank
        /// </summary>
        public void PlayRandomAudioClip()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioBankObj.GetAudioClip()); 
            }
        }
        public void PlaySpeficAudio(AudioClip audioClip)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip); 
            }
        }

        /// <summary>
        /// Stops the currently playing audio
        /// </summary>
        public void Stop() => audioSource.Stop();
    }
}
