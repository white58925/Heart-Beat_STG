using System.Collections;
using UnityEngine;
using Uduino;
using UnityEngine.SceneManagement;

public class UdinoController : MonoBehaviour
{
    int lastPin9ButtonValue = 1;
    int lastPin6ButtonValue = 0; 
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
        CheckButton9Pin();
        CheckButton6Pin();
        CheckAnalogValue();    
    }
    private void CheckButton9Pin()
    {
        int buttonValue = UduinoManager.Instance.digitalRead(9);
        if (buttonValue == 0 && lastPin9ButtonValue == 1)
        {
            if (PlayerPrefs.GetInt("Scene", 0) == 0)
            {
                EventManager.TriggerEvent("Arduino");
            }
            else
            {
                EventManager.TriggerEvent("UpArrow");
            }
            lastPin9ButtonValue = 0;
        }
        else if (buttonValue == 1 && lastPin9ButtonValue == 0)
        {
            lastPin9ButtonValue = 1;
        }
    }
    private void CheckButton6Pin()
    {
        int buttonValue = UduinoManager.Instance.digitalRead(6);
        if (buttonValue == 0 && lastPin6ButtonValue == 1)
        {
            EventManager.TriggerEvent("assistStart");
            lastPin6ButtonValue = 0;
        }
        else if (buttonValue == 1 && lastPin6ButtonValue == 0)
        {
            EventManager.TriggerEvent("assistStop");
            lastPin6ButtonValue = 1;
        }
    }
    private void CheckAnalogValue()
    {
        int analogValue = UduinoManager.Instance.analogRead(AnalogPin.A0);
        //Debug.LogError(analogValue);
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
