using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float tolerance = 200f;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    float boundary_xMin, boundary_xMax, boundary_yMin, boundary_yMax, padding = 0.1f;

    private bool DashBool;
    private float dashSpeed;
    public GameObject dashEffect;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundary_xMin, boundary_xMax),
        Mathf.Clamp(transform.position.y, boundary_yMin, boundary_yMax));
        if (Input.GetKeyDown("space"))
        {
            if (Ticker.instance.CheckTouchState() == TouchState.Excellent)
            {
                Debug.LogError("PlayerDash");
                Dash();
                Instantiate(dashEffect, transform.position, Quaternion.identity);
            }
            else if (Ticker.instance.CheckTouchState() == TouchState.Good)
            {
                Debug.LogError("PlayerGood");
            }
            else
            {
                Debug.LogError("PlayerBad");
            }
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, boundary_xMin, boundary_xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, boundary_yMin, boundary_yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        boundary_xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        boundary_xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        boundary_yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        boundary_yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Dash()
    {
        if (!DashBool)
        {
            float xRaw = Input.GetAxisRaw("Horizontal");
            float yRaw = Input.GetAxisRaw("Vertical");

            if (xRaw != 0 || yRaw != 0)
            {
                bc.enabled = false;
                StartCoroutine(DashNow(xRaw, yRaw));
            }
        }
    }

    IEnumerator DashNow(float x, float y)
    {

        DashBool = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        dashSpeed = 20;
        rb.velocity += dir.normalized * dashSpeed;

        yield return new WaitForSeconds(0.07f);

        dashSpeed = 0;
        rb.velocity = dir.normalized * dashSpeed;

        DashBool = false;
        bc.enabled = true;
    }
}

