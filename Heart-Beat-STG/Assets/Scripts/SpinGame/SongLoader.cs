using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongLoader : MonoBehaviour
{
    public List<LevelObject> levelObjectList = new List<LevelObject>();
    public LevelObject activeLevelObject;
    public AudioClip startSong;
    public AudioSource soundEffect;
    public AudioSource uiMusic;
    [Space(100)]
    public int songIndex;
    public SongType currentSongType;

    public static SongLoader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Double songloader instance");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < levelObjectList.Count)
        {
            activeLevelObject = levelObjectList[levelIndex];
        }
        else
        {
            Debug.LogError("Level is not existed");
        }
    }
    public void PlaySoundEffect()
    {
        soundEffect.Play();
    }
    public void PlayUIMusic(bool active)
    {
        if(active)
        {
            uiMusic.volume = 0.1f;
            uiMusic.clip = startSong;
            uiMusic.Play();
        }
        else
        {
            uiMusic.Stop();
        }
    }
    public void ChangeSong(int index)
    {
        uiMusic.Stop();
        uiMusic.clip = levelObjectList[index].fullSong;
        switch(index)
        {
            case 0:
                uiMusic.volume = 0.1f;
                break;
            case 1:
                uiMusic.volume = 0.4f;
                break;
            case 2:
                uiMusic.volume = 0.25f;
                break;
            case 3:
                uiMusic.volume = 0.4f;
                break;
        }
        uiMusic.Play();
    }
    //@TODO : TObedeleted
    public void LoadScene(int songIndexUncategorized, SongType songType)
    {
        
    }

    public bool NextSong()
    {
        return false;
    }
}
