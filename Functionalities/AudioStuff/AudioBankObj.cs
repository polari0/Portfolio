using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine;

namespace HexKeyGames
{
    [System.Serializable, CreateAssetMenu(fileName = "Audio Bank", menuName = "AudioBanks")]
    public class AudioBankObj : ScriptableObject
    {
        [Tooltip("For randomized Audio Clips")]
        public List<AudioClip> audioClipList = new List<AudioClip>();

        public AudioClip GetAudioClip()
        {
            AudioClip chosenClip = audioClipList.RandomElement();
            return chosenClip;
        }
    }
}
