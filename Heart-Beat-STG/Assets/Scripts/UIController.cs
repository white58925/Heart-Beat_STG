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
        SongLoader.instance.PlayUIMusic(true);
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
            OnClickEscapeButton();
        }
        else
        {
            if (!mainMenuGameObject.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    EventManager.TriggerEvent("RightArrow");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    EventManager.TriggerEvent("LefttArrow");
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UIButtonClick(UIButton.Start);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (PlayerPrefs.GetInt("useNoise", 1) == 1)
            {
                PlayerPrefs.SetInt("useNoise", 0);
            }
            else
            {
                PlayerPrefs.SetInt("useNoise", 1);
            }
        }               
    }
    public void UIButtonClick(UIButton uiButtonType)
    {
        SongLoader.instance.PlaySoundEffect();
        switch (uiButtonType)
        {
            case UIButton.Start:               
                uiState = UIState.CharacterSelect;
                mainMenuGameObject.SetActive(false);
                characterSelectGameObject.SetActive(true);
                characterSelect.HideCharacterInfo();
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
        SongLoader.instance.PlaySoundEffect();
        if (characterSelect.isShowingCharacterInfo)
        {
            SongLoader.instance.PlayUIMusic(false);
            PlayerPrefs.SetInt("Scene", 1);
            SongLoader.instance.LoadLevel(characterSelect.CurrentChar);
            SceneManager.LoadScene(2);
        }
        else
        {
            characterSelect.ShowCharacterInfo(characterSelect.CurrentChar);
        }       
    }
    public void ArduinoButtonClick()
    {
        SongLoader.instance.PlaySoundEffect();
        if (mainMenuGameObject.activeSelf)
        {
            UIButtonClick(UIButton.Start);
            //UIButtonClick((UIButton)index);
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
    public void OnClickEscapeButton()
    {
        if (uiState == UIState.MainMenu)
        {
            Application.Quit();
        }
        else
        {
            if (characterSelect.isShowingCharacterInfo)
            {
                characterSelect.HideCharacterInfo();
            }
            else
            {
                SongLoader.instance.PlayUIMusic(true);
                uiState = UIState.MainMenu;
                mainMenuGameObject.SetActive(true);
                characterSelectGameObject.SetActive(false);
                characterSelect.HideCharacterInfo();
                uiPrefabGameObject.SetActive(false);
            }
        }
    }
}
