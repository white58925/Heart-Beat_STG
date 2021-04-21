using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;
    private void Start()
    {
        background.color = SongLoader.instance.activeLevelObject.color;
        Instantiate(SongLoader.instance.activeLevelObject.visualEffectObject);
    }
}
