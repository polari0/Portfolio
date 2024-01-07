using HexKeyGames.Level;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexKeyGames
{
    public class SpeakerManager : MonoBehaviour
    {

        public float time;

        [SerializeField]
        private AudioBankObj a_15min, a_10min, a_5min, a_4min, a_3min, a_2min, a_1min, gameEndSound, RandomAudio;
        [SerializeField]
        private List<TimerAudioPlayer> speakers = new List<TimerAudioPlayer>();
        [SerializeField]
        private List<AudioClip> randomAudioCopy = new List<AudioClip>();

        /// <summary>
        /// In start we find all the speakers in the scene 
        /// </summary
        private void Start()
        {
            speakers = FindObjectsByType<TimerAudioPlayer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
            randomAudioCopy = RandomAudio.audioClipList.ToList();
        }
        /// <summary>
        /// This fuction chooses what audio to play at the correct time and then plays it from the active speakers 
        /// </summary>
        /// <param name="seconds"></param>
        public void TimerChooseAudio(int seconds)
        {
            int minutes = seconds / 60;

            var audioBank = minutes switch
            {
                15=> a_15min,
                10 => a_10min,
                5 => a_5min,
                4 => a_4min,
                3 => a_3min,
                2 => a_2min,
                1 => a_1min,
                0 => gameEndSound,
                _ => RandomAudio
            };

            AudioClip randomAudioClip;
            if (audioBank == RandomAudio)
            {
                randomAudioClip = randomAudioCopy.RandomElement();
                randomAudioCopy.Remove(randomAudioClip);
            }
            else
            {
                randomAudioClip = audioBank.audioClipList.RandomElement();
            }

            foreach (TimerAudioPlayer speaker in speakers)
            {
                if (speaker.enabled)
                    speaker.PlayAnnoucement(randomAudioClip);
            }

        }

        public void PlayAnnouncement(AudioClip clip)
        {
            foreach (TimerAudioPlayer speaker in speakers)
            {
                if (speaker.enabled)
                    speaker.PlayAnnoucement(clip);
            }
        }
    }
}
