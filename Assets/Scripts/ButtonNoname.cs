using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNoname : MonoBehaviour {

    public GameObject ObjOkname;
    Vector3 a = new Vector3();
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
        transform.localScale = a; ObjOkname.SetActive(false);
    }
}
