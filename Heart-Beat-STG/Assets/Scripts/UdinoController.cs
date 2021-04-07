using System.Collections;
using UnityEngine;
using Uduino;
using UnityEngine.SceneManagement;

public class UdinoController : MonoBehaviour
{
    int lastbuttonValue = 1;
    int pinIndex = 9;

    int maxAnalogValue = 670;
    int analogThreshold = 1;

    int analogIndex = 0;
    int lastAnalogIndex = 0;
    public static int analogRotationValue;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        analogThreshold = maxAnalogValue / 3;

        int analogValue = UduinoManager.Instance.analogRead(AnalogPin.A0);     
        analogIndex = GetAnaolgIndex(analogValue);
        lastAnalogIndex = analogIndex;
    }
    void Update()
    {
        int buttonValue = UduinoManager.Instance.digitalRead(pinIndex);
        if(buttonValue == 0 && lastbuttonValue == 1)
        {
            if(PlayerPrefs.GetInt("Scene", 0) == 0)
            {
                EventManager.TriggerEvent("Arduino");
            }
            else
            {
                EventManager.TriggerEvent("UpArrow");
            }
            lastbuttonValue = 0;
            //StartCoroutine(WaitSeconds(0.5f));
        }
        else if (buttonValue == 1 && lastbuttonValue == 0)
        {
            lastbuttonValue = 1;
        }
        int analogValue = UduinoManager.Instance.analogRead(AnalogPin.A0);
        analogIndex = GetAnaolgIndex(analogValue);
        analogRotationValue = analogValue;
        if (lastAnalogIndex != analogIndex)
        {
            lastAnalogIndex = analogIndex;
            if (PlayerPrefs.GetInt("Scene", 0) == 0 && UIController.Instance != null)
            {
                switch (UIController.Instance.uiState)
                {
                    case UIState.MainMenu:
                        UIController.Instance.index = analogIndex;
                        break;
                    case UIState.CharacterSelect:
                        UIController.Instance.SetCharacterSelect(analogIndex);
                        break;
                }
            }
            else
            {
                analogRotationValue = analogValue;
            }
        }
        
    }
    private int GetAnaolgIndex(int value)
    {
        if(value < analogThreshold)
        {
            return 0;
        }
        else if (value < 2 * analogThreshold)
        {
            return 1;
        }
        return 2;
    }
}
