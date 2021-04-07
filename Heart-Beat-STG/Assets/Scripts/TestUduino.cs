using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class TestUduino : MonoBehaviour
{
    public AudioSource noise;
    public AudioSource music;
    private int target = 100;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        int analogValue = UduinoManager.Instance.analogRead(AnalogPin.A0);
        noise.volume = (float)Mathf.Abs(analogValue - target) / 700f;
        music.volume = 1f - noise.volume;
        //Debug.LogError(analogValue);
    }
    void Update()
    {
        int analogValue = UduinoManager.Instance.analogRead(AnalogPin.A0);
        noise.volume = (float)Mathf.Abs(analogValue - target) / 700f;
        music.volume = 1f - noise.volume;
        //Debug.LogError(analogValue);
    }
}
