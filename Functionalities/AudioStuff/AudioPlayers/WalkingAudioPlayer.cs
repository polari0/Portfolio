using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class WalkingAudioPlayer : AudioPlayer
    {

        [SerializeField]
        AudioBankObj audioBankObjWalking, audioBankObjRunning;


        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
        }


        public void PlayWalkingSound()
        {
            audioSource.Stop();
            audioSource.clip = audioBankObjWalking.audioClipList.RandomElement();
            audioSource.Play();
        }


        public void PlayRunningSound()
        {
            audioSource.Stop();
            audioSource.clip = audioBankObjRunning.audioClipList.RandomElement();
            audioSource.Play();
        }
    }
}
