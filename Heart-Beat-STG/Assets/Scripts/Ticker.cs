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
public class TickerState
{
    public bool isPressed;
    public DateTime recordDateTime;
}
public class Ticker : MonoBehaviour
{
    // Start is called before the first frame update
    public static Ticker instance;
    public int BPM = 60;
    public int tolerance = 50;
    public bool isUsingPenalty = false;
    [Space]
    public bool isUsingSample;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip tickerOneShot;
    [SerializeField]
    private AudioClip music;
    private bool isLock;
    private List<TickerState> tickerState = new List<TickerState>();
    private int tickerIndex = 1;
    private float BPMSeconds = 1f;
   
    void Awake()
    {
        MakeInstance();
    }
    void Start()
    {
        audioSource.clip = isUsingSample ? tickerOneShot : music;
        BPMSeconds = 60f / (float)BPM;
        isLock = false;
        tickerState.Add(new TickerState { isPressed = false,recordDateTime = DateTime.Now});
        tickerState.Add(new TickerState { isPressed = false, recordDateTime = DateTime.Now });
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
        double timeSpanInMilliSeconds = GetTimeSpan(tickerState[tickerIndex].recordDateTime);
        if(!isLock && !tickerState[tickerIndex].isPressed)
        {
            if(timeSpanInMilliSeconds <= tolerance)
            {
                tickerState[tickerIndex].isPressed = true;
                return TouchState.Excellent;
            }
            else if(timeSpanInMilliSeconds <= 2 * tolerance)
            {
                tickerState[tickerIndex].isPressed = true;
                return TouchState.Good;
            }
        }
        timeSpanInMilliSeconds -= (double)1000 * BPMSeconds;
        int nextIndex = (tickerIndex + 1) % 2;
        if(!isLock && !tickerState[nextIndex].isPressed)
        {
            if (timeSpanInMilliSeconds >= -tolerance)
            {
                tickerState[nextIndex].isPressed = true;
                return TouchState.Excellent;
            }
            else if (timeSpanInMilliSeconds >= -2 * tolerance)
            {
                tickerState[nextIndex].isPressed = true;
                return TouchState.Good;
            }
        }
        if (isUsingPenalty)
        {
            StopCoroutine(LockCoroutine());
            StartCoroutine(LockCoroutine());
        } 
        return TouchState.Bad;
    }
    public double GetTimeSpan(DateTime recordDateTime)
    {
        DateTime nowDateTime = DateTime.Now;
        return nowDateTime.Subtract(recordDateTime).TotalMilliseconds;
    }
    IEnumerator LockCoroutine()
    {
        Debug.LogError("SetLOck");
        isLock = true;
        yield return new WaitForSecondsRealtime(BPMSeconds/4);
        Debug.LogError("UnLOck");
        isLock = false;
    }
    IEnumerator TickerCoroutine()
    {
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(BPMSeconds);
        if(!isUsingSample)
        {
            audioSource.Play();
        }
        while (true)
        {
            if(isUsingSample)
            {
                audioSource.Play();
            }
            tickerIndex = (tickerIndex + 1) % 2;
            tickerState[tickerIndex].recordDateTime = DateTime.Now;
            tickerState[(tickerIndex + 1) % 2].isPressed = false;
            yield return waitForSecondsRealtime;
        }
    }
}
