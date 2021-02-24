using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroyItSelf());
    }
    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
