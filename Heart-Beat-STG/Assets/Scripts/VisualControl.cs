using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualControl : MonoBehaviour
{
    private void Start()
    {
        Instantiate(SongLoader.instance.activeLevelObject.visualEffectObject);
    }
    public void CreateEndingPrefab()
    {
        Instantiate(SongLoader.instance.activeLevelObject.endingPrefab);
    }
}
