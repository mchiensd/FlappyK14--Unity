using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMoving : MonoBehaviour
{
    public static float MoveSpeed { get; set; }
    public float MoveRange;
    private Vector3 oldPos;


    // Use this for initialization
    void Start()
    {
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(-1 * Time.deltaTime * MoveSpeed, 0, 0));
        if (Vector3.Distance(oldPos, transform.position) > MoveRange)
        {
            transform.position = oldPos;

        }
    }
}


