using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class TimerAudioPlayer : AudioPlayer
    {

        [SerializeField, Tooltip("This should always be the player with Store music playing")]
        private AudioSource storeMusicSource;

        private AudioSource SyncTimer;

        private void Awake()
        {
            SyncTimer = GameObject.FindGameObjectWithTag("VoiceOfGod").GetComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(MusicSync());
        }

        public void PlayAnnoucement(AudioClip audioClip)
        {
            if (!audioSource.isPlaying)
            {
                PlaySpeficAudio(audioClip);
            }
            else return;
        }

        private void Update()
        {
            if (audioSource.isPlaying)
                storeMusicSource.volume = 0.1f;
            else
                storeMusicSource.volume = 1f;

        }

        IEnumerator MusicSync()
        {
            while (storeMusicSource.enabled)
            {
                storeMusicSource.Stop();
                storeMusicSource.timeSamples = SyncTimer.timeSamples;
                storeMusicSource.Play();
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
