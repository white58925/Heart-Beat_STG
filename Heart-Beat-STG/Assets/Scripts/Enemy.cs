using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 1f;
    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    private void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Vector3 bulletsPos = new Vector3(transform.position.x, transform.position.y, -1);
            Instantiate(bullet, bulletsPos, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
