using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CameraExercise : MonoBehaviour
{
    [SerializeField] Image cueImage;
    private Animator anim;

    private bool DashBool;

    DateTime recordDateTime;
    DateTime startDateTime;
    public float tolerance = 100f;


    void Start()
    {
        anim = GetComponent<Animator>();

        startDateTime = DateTime.Now;
        StartCoroutine(Ticker());
    }

    // Update is called once per frame
    void Update()
    {
        DateTime nowDateTime = DateTime.Now;
        TimeSpan timeSpan = nowDateTime.Subtract(recordDateTime);
        float timeSpanInMilliSeconds = timeSpan.Milliseconds;

        float colorHint = timeSpanInMilliSeconds / 1000f;
        cueImage.color = new Color(colorHint, colorHint, colorHint, 1f);

        anim.SetBool("isDashing", false);

        if (timeSpanInMilliSeconds < tolerance)
        {
            cueImage.color = Color.red;
        }

        if (Input.GetKeyDown("space"))
        {
            if (timeSpanInMilliSeconds < tolerance)
            {
                anim.SetBool("isDashing", true);
            }
        }
    }

    IEnumerator Ticker()
    {
        while (true)
        {
            recordDateTime = DateTime.Now;
            TimeSpan timeSpan = recordDateTime.Subtract(startDateTime);
            yield return new WaitForSeconds(1);
        }
    }
}
