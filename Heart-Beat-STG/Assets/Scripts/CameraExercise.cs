using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CameraExercise : MonoBehaviour
{
    [SerializeField] Image cueImage;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float colorHint = (float)Ticker.instance.GetTimeSpanInMilliSeconds() / 1000f;
        cueImage.color = new Color(colorHint, colorHint, colorHint, 1f);

        anim.SetBool("isDashing", false);
        if (Input.GetKeyDown("space"))
        {
            if (Ticker.instance.CheckTouchState() == TouchState.Excellent)
            {
                Debug.LogError("CameraDash");
                anim.SetBool("isDashing", true);
            }
            else if (Ticker.instance.CheckTouchState() == TouchState.Good)
            {
                Debug.LogError("CameraGood");
            }
            else
            {
                Debug.LogError("CameraBad");
            }
        }
    }

}
