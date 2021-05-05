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
public enum UIState
{
    MainMenu = 0,
    CharacterSelect = 1
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
    public int index;
    public UIState uiState = UIState.MainMenu;
    private bool keyDown = false;
    private bool isGameStarted = false;
    
    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Scene", 0);
    }
    private void Start()
    {
        EventManager.StartListening("Arduino", ArduinoButtonClick);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("Arduino", ArduinoButtonClick);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Horizontal") > 0)
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
                else if (Input.GetAxis("Horizontal") < 0)
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
                StartGame();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
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
                uiState = UIState.CharacterSelect;
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
    public void StartGame()
    {
        PlayerPrefs.SetInt("Scene", 1);
        SongLoader.instance.LoadLevel(characterSelect.CurrentChar);
        //SongLoader.instance.LoadScene(1, SongType.Dance);

        SceneManager.LoadScene(1);
    }
    public void ArduinoButtonClick()
    {
        if(mainMenuGameObject.activeSelf)
        {
            UIButtonClick((UIButton)index);
        }
        else
        {
            StartGame();
        }
    }
    public void SetCharacterSelect(int value)
    {
        characterSelect.SetChar(value);
    }
}
