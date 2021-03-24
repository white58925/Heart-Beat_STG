using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;

public class UdinoController : MonoBehaviour
{
    int lastbuttonValue = 1;
    void Start()
    {
        UduinoManager.Instance.pinMode(8, PinMode.Input_pullup);
        //StartCoroutine(Tap());
    }
    IEnumerator Tap()
    {
        int buttonValue = UduinoManager.Instance.digitalRead(8);
        while(true)
        {
            if (lastbuttonValue == 1 && buttonValue == 0)
            {
                lastbuttonValue = 0;
                Debug.LogError("last" + lastbuttonValue);
                EventManager.TriggerEvent("UpArrow");
                yield return new WaitForSeconds(0.5f);   
            }
            else
            {
                lastbuttonValue = 1;
                Debug.LogError("last ->" + lastbuttonValue);
            }
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        int buttonValue = UduinoManager.Instance.digitalRead(8);

        if (lastbuttonValue != buttonValue)
        {
            if(buttonValue == 0)
            {
                EventManager.TriggerEvent("UpArrow");
                lastbuttonValue = buttonValue;
            }
            else
            {
                lastbuttonValue = buttonValue;
            }
        }

        if (buttonValue == 0)
        {
            
            Debug.LogError("0");
            
        }
        else
        {
            Debug.LogError("1");
        }
    }
}
