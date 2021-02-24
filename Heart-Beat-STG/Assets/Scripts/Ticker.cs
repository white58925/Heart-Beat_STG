using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum TouchState
{
    Excellent,
    Good,
    Bad
}
public class Ticker : MonoBehaviour
{
    // Start is called before the first frame update
    public static Ticker instance;
    public int BPM = 60;
    public int tolerance = 50;
    DateTime recordDateTime;
    DateTime startDateTime;
    private float BPMSeconds = 1f;
    void Awake()
    {
        MakeInstance();
    }
    void Start()
    {
        BPMSeconds = 60f / (float)BPM;
        StartCoroutine(TickerCoroutine());
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    public TouchState CheckTouchState()
    {
        double timeSpanInMilliSeconds = GetTimeSpanInMilliSeconds();
        if(timeSpanInMilliSeconds <= tolerance)
        {
            return TouchState.Excellent;
        }
        else if(timeSpanInMilliSeconds <= 2 * tolerance)
        {
            return TouchState.Good;
        }
        timeSpanInMilliSeconds -= (double)1000;
        if (timeSpanInMilliSeconds >= -tolerance)
        {
            return TouchState.Excellent;
        }
        else if (timeSpanInMilliSeconds >= -2 * tolerance)
        {
            return TouchState.Good;
        }
        return TouchState.Bad;
    }
    public double GetTimeSpanInMilliSeconds()
    {
        DateTime nowDateTime = DateTime.Now;
        TimeSpan timeSpan = nowDateTime.Subtract(recordDateTime);
        return timeSpan.TotalMilliseconds;
    }

    IEnumerator TickerCoroutine()
    {
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(BPMSeconds);
        while (true)
        {
            recordDateTime = DateTime.Now;
            TimeSpan timeSpan = recordDateTime.Subtract(startDateTime);
            yield return waitForSecondsRealtime;
        }
    }
}
