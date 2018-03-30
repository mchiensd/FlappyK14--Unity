using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeMoving : MonoBehaviour
{
    public static float MoveSpeed {get;set;}
    public float minY = -1;
    public float maxY = 1;
    public float oldPos;
   public static float movey { get; set; }


    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector3(-1 * Time.deltaTime * MoveSpeed, movey, 0));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("RSW"))
        {            
            transform.position = new Vector3(oldPos, Random.Range(minY, maxY), 0);
        }
    }

}
