﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EJChangePosition : MonoBehaviour
{
    public List <GameObject> eyes;
    
    void Start()
    {
        StartCoroutine(RandomChangePosition());
    }
    IEnumerator RandomChangePosition()
    {
        while(true)
        {
            foreach (GameObject eye in eyes)
            {
                eye.SetActive(true);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject eye in eyes)
            {
                eye.SetActive(false);
                Vector3 pos = new Vector3(Random.Range(9f, -9f), Random.Range(5f, -5f), 0);
                eye.transform.position = pos;
            }
        }
    }
}