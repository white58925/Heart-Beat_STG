﻿using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentCar;

    private void Awake()
    {
        SelectCar(0);
    }

    private void SelectCar(int index)
    {
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void ChangeCar(int change)
    {
        currentCar += change;
        SelectCar(currentCar);
    }
}
