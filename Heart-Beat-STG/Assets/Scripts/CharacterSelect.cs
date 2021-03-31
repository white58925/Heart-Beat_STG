﻿using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentChar;
    private int characterCount = 3;

    public int CurrentChar
    {
        get { return currentChar; }
    }
    private void Awake()
    {
        SelectChar(0);
    }

    private void SelectChar(int index)
    {
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void ChangeChar(int change)
    {
        currentChar += change;
        currentChar = currentChar == characterCount ? 0 : currentChar == -1 ? characterCount - 1 : currentChar;
        SelectChar(currentChar);
    }
}