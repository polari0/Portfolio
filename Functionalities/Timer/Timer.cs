using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class Timer : MonoBehaviour
 {
    [SerializeField]
    private TextMeshProUGUI timerText;

    [Header("Timer settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Setting")]
    public bool hasLimit;
    public float timerLimit;

    [Header("FormatSettings")]
    public bool isSecondsTimer;
    public TimerFormats formats;
    public Dictionary<TimerFormats, string> timerFormats = new Dictionary<TimerFormats, string>();

    private bool timeOver = true;


    void Start()
    {
            timerFormats.Add(TimerFormats.Whole, "0");
            timerFormats.Add(TimerFormats.TenthDecimal, "0.0");
            timerFormats.Add(TimerFormats.HundrathsDecimal, "0.00");
    }


    void Update()
    {

        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }
        else if (hasLimit && ((countDown && currentTime >= timerLimit) || (!countDown && currentTime <= timerLimit)))
        {
            if (timeOver)
            {
                TimeOver();
                timeOver = false;
            }
        }

        SetTimerText();
    }


    private void SetTimerText()
    {
        if (!isSecondsTimer)
        {
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float second = Mathf.FloorToInt(currentTime % 60);

            timerText.text = string.Format("{0:00} : {1:00}", minutes, second);
        }
        else
            timerText.text = isSecondsTimer ? currentTime.ToString(timerFormats[formats]) : currentTime.ToString();
    }


    private void TimeOver()
    {
        Debug.Log("TimeOver");
    }
}
public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrathsDecimal,
};