using System.Collections;
using UnityEngine;
using Uduino;
using System;
using UnityEngine.SceneManagement;

public class UdinoController : MonoBehaviour
{
    [HideInInspector]
    public static int analogRotationValue = 0;

    private int roatationThreshold = 10;
    private int rotationInitialValue;
    private int characterCount = 4;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        UduinoManager.Instance.OnDataReceived += DataReceived;
    }
    private void Start()
    {
        rotationInitialValue = analogRotationValue;
    }
    private int GetAnaolgIndex(int value)
    {
        if(analogRotationValue >= rotationInitialValue)
        {
            return (analogRotationValue - rotationInitialValue) / roatationThreshold % characterCount;
        }
        else
        {
            int a = Mathf.Abs(analogRotationValue - rotationInitialValue) / roatationThreshold % characterCount;
            return a == 3 ? 1 : a == 1 ? 3 : a; 
        }
    }
    void DataReceived(string data, UduinoDevice board)
    {
        int number;
        if (Int32.TryParse(data, out number))
        {
            if (number == 10000 || number == 10001)
            {
                if (number == 10000)
                {
                    if (PlayerPrefs.GetInt("Scene", 0) == 0)
                    {
                        if (UIController.Instance.uiState == UIState.MainMenu)
                        {
                            rotationInitialValue = analogRotationValue;
                        }
                        EventManager.TriggerEvent("ArduinoEnterButton");
                    }
                    else
                    {
                        EventManager.TriggerEvent("ArduinoEnterButton");
                        EventManager.TriggerEvent("UpArrow");
                    }
                }
            }
            else if (number == 10003 || number == 10004)
            {
                if (number == 10003)
                {
                    EventManager.TriggerEvent("ArduinoEscapeButton");
                }
            }
            else
            {
                analogRotationValue = number;
                if (PlayerPrefs.GetInt("Scene", 0) == 0)
                {
                    if(UIController.Instance.uiState == UIState.CharacterSelect)
                    {
                        UIController.Instance.SetCharacterSelect(GetAnaolgIndex(analogRotationValue));
                    }
                }
            }
        }
    }
}
