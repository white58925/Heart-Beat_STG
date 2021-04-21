using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Object")]
public class LevelObject : ScriptableObject
{
    [Header("Level info")]
    
    public GameObject visualEffectObject;
    public SongObject songObject;
    public Color color;
}
