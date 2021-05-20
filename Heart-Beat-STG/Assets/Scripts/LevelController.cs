using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    public static LevelController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Double songloader instance");
            Destroy(gameObject);
        }
    }
    public void SetPauseMenu(bool isActive)
    {
        Time.timeScale = isActive ? 0f : 1f;
        pauseMenu.SetActive(isActive);
    }
    public bool IsShowingPauseMenu()
    {
        return pauseMenu.activeSelf;
    }
}
