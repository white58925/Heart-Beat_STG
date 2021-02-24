using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SampleBeat : MonoBehaviour
{
    
    public float tolerance = 100f;
    public int gameTime = 60;
    [SerializeField] Text pointText;
    [SerializeField] Image cueImage;
    [SerializeField] Button startButton;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    DateTime recordDateTime;
    DateTime startDateTime;
    bool isInGame = false;
    int point = 0;
    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }
    //Test Miro Git
    void Update()
    {
        if (isInGame)
        { 
            DateTime nowDateTime = DateTime.Now;
            TimeSpan timeSpan = nowDateTime.Subtract(recordDateTime);
            float timeSpanInMilliSeconds = timeSpan.Milliseconds;
            float colorHint = timeSpanInMilliSeconds / 1000f;
            cueImage.color = new Color(colorHint, colorHint, colorHint, 1f);
            if (timeSpanInMilliSeconds < tolerance)
            {
                cueImage.color = Color.red;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (timeSpanInMilliSeconds < tolerance)
                {
                    point++;
                    pointText.text = point.ToString();
                }
                else
                {
                    Debug.LogError("Not in Time Range");
                }

            }
        }
    }
    public void OnStartButtonClick()
    {
        if(!isInGame)
        {
            startButton.gameObject.SetActive(false);
            isInGame = true;
            point = 0;
            pointText.text = "0";
            startDateTime = DateTime.Now;
            StartCoroutine(Ticker());
        }  
    }
    IEnumerator Ticker()
    {
        while (true)
        {
            recordDateTime = DateTime.Now;
            TimeSpan timeSpan = recordDateTime.Subtract(startDateTime);
            audioSource.PlayOneShot(audioClip);
            if (timeSpan.Seconds > gameTime)
            {
                isInGame = false;
                startButton.gameObject.SetActive(true);
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
