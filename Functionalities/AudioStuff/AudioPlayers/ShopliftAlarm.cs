using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class ShopliftAlarm : MonoBehaviour // : AudioPlayer
    {
        [SerializeField] private AudioClip theftAlarm, theftAnnouncement, nuclearAler;
        [SerializeField] private AudioSource[] theftAlarmSources;
        [SerializeField] private SpeakerManager speakerManager;

        private void Start()
        {
            if (speakerManager == null) speakerManager = FindAnyObjectByType<SpeakerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Character>(out _))
            {
                StartCoroutine(AlarmStart());
                speakerManager.PlayAnnouncement(theftAnnouncement);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Character>(out _))
            {
                foreach (var theftAlarmSource in theftAlarmSources)
                    theftAlarmSource.Stop();
            }
        }

        private IEnumerator AlarmStart()
        {
            yield return new WaitForSeconds(0.1f);

            var clip = UnityEngine.Random.Range(0, 100) == 0 ? nuclearAler : theftAlarm;

            foreach (var theftAlarmSource in theftAlarmSources)
            {
                theftAlarmSource.clip = clip;
                theftAlarmSource.timeSamples = 0;
                theftAlarmSource.Play();
            }
        }
    }
}
