using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRePlay : MonoBehaviour
{

    
    Vector3 a = new Vector3();
    // Use this for initialization
    void Start()
    {
        a = new Vector3(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
    public void OnMouseDown()
    {
        //if (Input.GetMouseButtonDown(0))
        {
            transform.localScale = new Vector3(transform.localScale.x + (transform.localScale.x / 100 * 5), transform.localScale.y + (transform.localScale.y / 100 * 5));
        }
       
    }
    public void OnMouseUp()
    {
       // if (Input.GetMouseButtonUp(0))
        {
            
            Debug.Log("Replay: " + Button.aa);
            transform.localScale = a; GameStateManager.GameState = GameState.Intro; Application.LoadLevel(Application.loadedLevelName);
        }
      
    }

}
