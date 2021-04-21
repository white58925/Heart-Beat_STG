using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using System;
public class ReadEncoder : MonoBehaviour
{
    public static int analogRotationValue;
    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
    }

    void DataReceived(string data, UduinoDevice board)
    {
        int number;   
        if(Int32.TryParse(data, out number))
        {
            analogRotationValue = number;
        }
        //Debug.LogError(data);
    }
}
