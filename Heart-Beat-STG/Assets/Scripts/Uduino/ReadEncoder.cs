using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ReadEncoder : MonoBehaviour
{
    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
    }

    void DataReceived(string data, UduinoDevice board)
    {
        Debug.Log(data);
    }
}
