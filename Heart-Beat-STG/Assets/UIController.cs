using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIButton
{
    Start = 0,
    Option = 1,
    Exit = 2
}

public class UIController : MonoBehaviour
{
    [SerializeField] int maxIndex;
    [SerializeField] CharacterSelect characterSelect;
    [Header("GameObject")]
    [SerializeField] GameObject mainMenuGameObject;
    [SerializeField] GameObject characterSelectGameObject;
    [SerializeField] GameObject uiPrefabGameObject;

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
        if(!mainMenuGameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.LogError(characterSelect.CurrentChar + " Chracter Start Game");
                SongLoader.instance.LoadScene(1, SongType.Dance);
                SceneManager.LoadScene(1);


            }else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                characterSelect.ChangeChar(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                characterSelect.ChangeChar(-1);
            }

        }
    }
    public void UIButtonClick(UIButton uiButtonType)
    {
        switch(uiButtonType)
        {
            case UIButton.Start:
                mainMenuGameObject.SetActive(false);
                characterSelectGameObject.SetActive(true);
                uiPrefabGameObject.SetActive(true);
                break;
            case UIButton.Option:
                break;
            case UIButton.Exit:
                Application.Quit();
                break;
        }
    }
}
