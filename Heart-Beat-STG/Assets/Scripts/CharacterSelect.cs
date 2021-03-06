﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private List<GameObject> characterInfoList = new List<GameObject>();
    public bool isShowingCharacterInfo = false;
    private int currentChar;
    private int characterCount = 4;

    public int CurrentChar
    {
        get { return currentChar; }
    }
    private void Awake()
    {
        SelectChar(0);
        EventManager.StartListening("RightArrow", OnClickRightArrow);
        EventManager.StartListening("LefttArrow", OnClickLeftArrow);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("RightArrow", OnClickRightArrow);
        EventManager.StopListening("LefttArrow", OnClickLeftArrow);
    }
    private void OnClickRightArrow()
    {
        if(isShowingCharacterInfo)
        {

        }
        else
        {
            ChangeChar(1);
        }
    }
    private void OnClickLeftArrow()
    {
        if (isShowingCharacterInfo)
        {

        }
        else
        {
            ChangeChar(-1);
        }
    }
    private void SelectChar(int index)
    { 
        SongLoader.instance.ChangeSong(index);
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void ChangeChar(int change)
    {
        if (isShowingCharacterInfo) return;
        currentChar += change;
        currentChar = currentChar == characterCount ? 0 : currentChar == -1 ? characterCount - 1 : currentChar;
        SelectChar(currentChar);
    }
    public void SetChar(int value)
    {
        if(currentChar != value && !isShowingCharacterInfo)
        {
            currentChar = value;
            SelectChar(currentChar);
        }
    }
    public void ShowCharacterInfo(int index)
    {
        isShowingCharacterInfo = true;
        characterInfoList[index].SetActive(true);
    }
    public void HideCharacterInfo()
    {
        isShowingCharacterInfo = false;
        foreach (GameObject gameObject in characterInfoList)
        {
            gameObject.SetActive(false);
        }
    }
}
