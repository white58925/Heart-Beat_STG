using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIButton
{
    Start = 0,
    Option = 1,
    Exit = 2
}

public class UIController : MonoBehaviour
{
    [SerializeField] int maxIndex;
    [Header("GameObject")]
    [SerializeField] GameObject characterSelect;
    [SerializeField] GameObject UIPrefab;
    public static UIController Instance;
    private int index;
    private bool keyDown = false;
    
    public int Index
    {
        get { return index; }
    }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }
    public void UIButtonClick(UIButton uiButtonType)
    {
        Debug.LogError(uiButtonType + "click");
        switch(uiButtonType)
        {
            case UIButton.Start:
               
                break;
            case UIButton.Option:
                break;
            case UIButton.Exit:
                Application.Quit();
                break;
        }
    }
}
