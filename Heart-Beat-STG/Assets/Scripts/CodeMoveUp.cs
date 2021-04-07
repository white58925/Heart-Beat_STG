using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMoveUp : MonoBehaviour
{
    Vector3 Pos;

    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, Random.Range(0.1f, 0.05f), 0);
        transform.position += move;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        transform.position = Pos;
    }
}
