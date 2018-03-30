using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttondelete : MonoBehaviour {
    private static Buttondelete m_instance;
    Vector3 a = new Vector3();
   public GameObject CanvasDelete;

    public static Buttondelete _instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = null;
            }
            return m_instance;
        }
    }

    // Use this for initialization
    void Start () {
        a = new Vector3(transform.localScale.x, transform.localScale.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseDown()
    {
        transform.localScale = new Vector3(transform.localScale.x + (transform.localScale.x / 100 * 5), transform.localScale.y + (transform.localScale.y / 100 * 5));

    }
    public void OnMouseUp()
    {

        transform.localScale = a;
        CanvasDelete.SetActive(false);
        Debug.Log("Deleted!!!");
        checkreset.checkrss = 1;
        Debug.Log("Deleted2!!!");
        HC._instance.ClearLeaderBoard();
           Debug.Log("Deleted3!!!");
        
     
    }
}
