using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Executer
{
    public float StartTime, EndTime;
    public GameObject[] Objs;
}

public class Manager : MonoBehaviour
{
    private int Event;

    public static bool GameEnd;
    private bool ExecuteOnce;

    public static float BackgroundTimer;
    public Executer[] TimeExecuter;
    private void Start()
    {
        foreach(Executer executer in TimeExecuter)
        {
            StartCoroutine(AnimationCorountine(executer));
        }

    }
    IEnumerator AnimationCorountine(Executer executer)
    {
        yield return new WaitForSeconds(executer.StartTime);
        foreach (GameObject obj in executer.Objs)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(executer.EndTime - executer.StartTime);
        foreach (GameObject obj in executer.Objs)
        {
            obj.SetActive(false);
        }
    }
    void Update()
    {
        //BackgroundTimer += Time.deltaTime;

        //if (!GameEnd)
        //{
        //    if (Event < TimeExecuter.Length)
        //    {
        //        if (!ExecuteOnce)
        //        {
        //            if (BackgroundTimer >= TimeExecuter[Event].StartTime && BackgroundTimer <= TimeExecuter[Event].EndTime)
        //            {
        //                foreach (GameObject obj in TimeExecuter[Event].Objs)
        //                {
        //                    obj.SetActive(true);
        //                }

        //                ExecuteOnce = true;
        //            }
        //        }

        //        if (BackgroundTimer > TimeExecuter[Event].EndTime)
        //        {
        //            Event += 1;
        //            ExecuteOnce = false;
        //        }
        //    }

        //}
    }
}
