using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {
    public Button.ButtonType bt;
    public Button() { }
    public static string N;
    public Text name;
    public GameObject ObjNoname, StageSG, StageHN,StageMT;
    Vector3 a = new Vector3();
    public static int aa { get; set; }
    public  void OnMouseDown()
    {
        N = name.text.ToString();
        transform.localScale = new Vector3(transform.localScale.x + (transform.localScale.x / 100 * 5), transform.localScale.y + (transform.localScale.y / 100 * 5));


    }
    public  void OnMouseUp()
    {
      
      //  HighScore.Load();
        Debug.Log("StateInt: " + ScrollSnapRect.StateInt);
        Debug.Log("CharInt: " + ScrollSnapRectC.StateIntC);
        Debug.Log("Name: "+N);
        transform.localScale = a;
        if (bt == ButtonType.btnPlay && Button.N != "")
        {
            
           
            Debug.Log("Playing Game"); Application.LoadLevel("GamePlay");
        }
               // Application.LoadLevel("GamePlay");
            else
        {
            ObjNoname.SetActive(true);
        }
        if (bt == ButtonType.btnRePlay)
        {
           Application.LoadLevel("GamePlay");
        }
      

    }
    public void Awake()
    {
        
        BackgroundMoving.MoveSpeed = 0.5f;
        GrassMoving.MoveSpeed = 2;

    }
    // Use this for initialization
    void Start() {
        
        ObjNoname.SetActive(false);
        a = new Vector3(transform.localScale.x, transform.localScale.y);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.backButtonLeavesApp)
        {
            Application.Quit();
        }
        if (ScrollSnapRect.StateInt == 0)
        {
            StageSG.SetActive(true);
            StageMT.SetActive(false);
            StageHN.SetActive(false);

        }
        if (ScrollSnapRect.StateInt == 1)
        {
            StageSG.SetActive(false);
            StageMT.SetActive(true);
            StageHN.SetActive(false);
        }
        if (ScrollSnapRect.StateInt == 2)
        {
            StageSG.SetActive(false);
            StageMT.SetActive(false);
            StageHN.SetActive(true);
        }
    }
    public enum ButtonType
        {
        btnPlay, btnRePlay, btnOp,btnCloseOp
        }
}
  



//


