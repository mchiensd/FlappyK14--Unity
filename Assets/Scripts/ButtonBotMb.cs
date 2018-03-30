﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBotMb : MonoBehaviour
{
    public ButtonBotMb.ButtonType bt;
    public ButtonBotMb() { }
    public Animator myani;
    public static int j { get; set; }
    //    public static bool soundck = true;
    Vector3 a = new Vector3();
    GameObject Ani;
    public GameObject Delete, About;
    public void OnMouseDown()
    {

        transform.localScale = new Vector3(transform.localScale.x + (transform.localScale.x / 100 * 5), transform.localScale.y + (transform.localScale.y / 100 * 5));


    }
    int i = 0;
    //  int j = 0;
    public void OnMouseUp()
    {

        transform.localScale = a;


        {
            if (bt == ButtonType.btnGearPressSound && j == 0)
            {
                Debug.Log("SoundOff");
                myani.SetTrigger("GearMbSoundOff"); j = 1;
                ScoreManage.checksound = false;
            }
            else
            {
                if (bt == ButtonType.btnGearPressSound && j == 1)
                {
                    Debug.Log("SoundOn");
                    myani.SetTrigger("GearMbSoundOn"); j = 0; ScoreManage.checksound = true;
                    SoundsController.PlaySound(soundsGame.onsound);
                }
            }

            if (bt == ButtonType.btnGearOpen && i == 1)
            {
                Debug.Log("Hide Bar");
                myani.SetTrigger("GearMbNor"); i = 0;
            }
            else
            {
                if (bt == ButtonType.btnGearOpen && i == 0 && ScoreManage.checksound == true)
                {
                    Debug.Log("OpenBar");
                    myani.SetTrigger("GearMbOpen"); i = 1;
                }
                else
                    if (bt == ButtonType.btnGearOpen && i == 0 && ScoreManage.checksound == false)
                {
                    Debug.Log("OpenSoundOff");
                    myani.SetTrigger("GearMbOpenSoundOff"); i = 1; j = 1;
                }
            }
            if (bt == ButtonType.btnDel)
            {
                Delete.SetActive(true);
                Debug.Log("Del");

            }


        }

        if (bt == ButtonType.btnExit)
        {

            Application.Quit();
            Debug.Log("Exit");
        }
        if (bt == ButtonType.btnAbout)
        {
            About.SetActive(true);
            Debug.Log("About");
           

        }

    }
    public void Awake()
    {
        Ani = GameObject.FindGameObjectWithTag("Ani");
        myani = Ani.GetComponent<Animator>();

    }
    // Use this for initialization
    void Start()
    {



        //  ObjOp.SetActive(false);
        a = new Vector3(transform.localScale.x, transform.localScale.y);

    }


    // Update is called once per frame
    void Update()
    {

    }
    public enum ButtonType
    {
        btnGearNor, btnGearOpen, btnGearPressSound, btnDel, btnExit, btnAbout
    }
}




//


