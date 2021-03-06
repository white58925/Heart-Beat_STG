﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Level Object")]
public class LevelObject : ScriptableObject
{
    [Header("Tutorial")]
    public bool isTutorial;
    [Header("Level info")]
    public GameObject visualEffectObject;
    public SongObject songObject;
    public AudioClip fullSong;
    [Header("Circle info")]
    public GameObject circlePrefab;
    [Header("Ending")]
    public GameObject endingPrefab;
}
