using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroyItSelf());
    }
    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
