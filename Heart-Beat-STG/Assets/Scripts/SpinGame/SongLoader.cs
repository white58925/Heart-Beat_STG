using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongLoader : MonoBehaviour
{
    public List<LevelObject> levelObjectList = new List<LevelObject>();
    public LevelObject activeLevelObject;
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
    //@TODO : TObedeleted
    public void LoadScene(int songIndexUncategorized, SongType songType)
    {
        
    }

    public bool NextSong()
    {
        return false;
    }
}
