using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum TouchState
{
    Excellent,
    Good,
    Bad
}
public class Ticker : MonoBehaviour
{
    // Start is called before the first frame update
    public static Ticker instance;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    private
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
