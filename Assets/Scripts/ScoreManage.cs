using UnityEngine;
using System.Collections;

public class ScoreManage : MonoBehaviour
{

    
    public static bool checksound = true;
    public static int Score { get; set; }
    public Animator Ani;

    // Use this for initialization

        void CheckSound()
    {
        if (checksound == true)
        {
            audio.volume = 1;
            audioall.volume = 1;
        }
        else
         if (checksound == false)
        {
            audio.volume = 0;
            audioall.volume = 0;
        }
    }
    void Start()
    {
        StagePlayMode = Random.Range(1, 4);
        Debug.Log(StagePlayMode);
        CheckSound();

        if (ScrollSnapRect.StateInt == 0) HN.gameObject.SetActive(false);
        if (ScrollSnapRect.StateInt == 1) SG.gameObject.SetActive(false);
        (Tens.gameObject as GameObject).SetActive(false);
        (Hundreds.gameObject as GameObject).SetActive(false);
        ObjRain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (previousScore != Score) //save perf from non needed calculations
        {
            if (Score < 10)
            {
                //just draw units
                Units.sprite = numberSprites[Score];
            }
            else if (Score >= 10 && Score < 100)
            {
                Units.transform.position = new Vector3(0.4f, Tens.transform.position.y, 0f);

                (Tens.gameObject as GameObject).SetActive(true);
                
                Tens.sprite = numberSprites[Score / 10];
                Units.sprite = numberSprites[Score % 10];
            }
            else if (Score >= 100)
            {
                (Tens.gameObject as GameObject).SetActive(true);
                (Hundreds.gameObject as GameObject).SetActive(true);
                Units.transform.position = new Vector3(0.55f, Tens.transform.position.y, 0f);
                Tens.transform.position = new Vector3(-0.1f, Tens.transform.position.y, 0f);
                Hundreds.transform.position = new Vector3(-0.65f, Tens.transform.position.y, 0f);


                Hundreds.sprite = numberSprites[Score / 100];
                int rest = Score % 100;
                Tens.sprite = numberSprites[rest / 10];
                Units.sprite = numberSprites[rest % 10];
            }
           
        }
      
        StageInPlay();
    }

    int StagePlayMode;
    void StageInPlay()
    {
        if(StagePlayMode == 1)
        {
            EventSun(10, 30);
            EventRain(35, 60);
            EventRo(65, 95);
            EventMoveY(100, 120, 140);
            EventRain(141, 160);
            EventSun(163, 180);
            EventRo(181, 200);
            //=======
            EventSun(210, 230);
            EventRain(235, 260);
            EventRo(265, 295);
            EventMoveY(300, 320, 340);
            EventRain(341, 360);
            EventSun(363, 380);
            EventRo(381, 400);
            //----------
            EventSun(410, 430);
            EventRain(435, 460);
            EventRo(465, 495);
            EventMoveY(500, 520, 540);
            EventRain(541, 560);
            EventSun(563, 580);
            EventRo(581, 600);

        }
        if (StagePlayMode  == 2)
        {
            EventRain(10, 35);
            EventSun(39, 70);
            EventRo(73, 99);
            EventMoveY(100, 120, 150);
            EventSun(155, 199);
            EventRain(210, 235);
            EventSun(239, 270);
            EventRo(275, 299);
            EventMoveY(300, 330, 350);
            EventSun(355, 400);
            EventRo(410, 450);
        }
        if (StagePlayMode  == 3)
        {
            EventRo(10, 40);
            EventSun(45, 80);
            EventRain(85, 120);
            EventMoveY(130, 150, 170);
            EventSun(180, 210);
            EventRo(210, 240);
            EventSun(245, 280);
            EventRain(285, 320);
            EventMoveY(330, 350, 370);
            EventSun(380, 410);
        }
    }

    void EventMoveY(int a,int b, int c)
    {
        if (Score == a)
        {
            TreeMoving.movey = 0.004f;

        }

        if (Score == b)
        {
            TreeMoving.movey = -0.004f;

        }
        if (Score == c)
        {
            TreeMoving.movey = 0f;

        }
    }
    void EventRo(int a2, int b2)
    {
        if(Score == a2)
        {
            Ani.SetTrigger("PipesRo");
        }
        else
        {
            if(Score == b2)
            {
                Ani.SetTrigger("PipesNor");
            }
        }
    }
    void EventSun(int a1, int b1)
    {
        if (Score == a1 )
        {
            Light.SetActive(true);

        }
        if (Score == b1)
        {
            Light.SetActive(false);

        }
    }
    int i=0;
    void EventRain(int StartRain,int EndRain)
    {
        if (ScrollSnapRect.StateInt == 2)
        {

            if (Score == StartRain || Score == StartRain + 100 || Score == StartRain + 200 || Score == StartRain + 300 || Score == StartRain + 400 || Score == StartRain + 500 || Score == StartRain + 600 || Score == StartRain + 700 || Score == StartRain + 800)
            {
                audio.volume += 0.05f;
                if (!audio.isPlaying)
                {

                    //  MB.SetTrigger("PlanNight");
                    SoundsControllerRain.PlaySound(soundsGameRain.Fade);
                    aniHN.SetTrigger("HNTT");


                }
                aniHN.SetTrigger("HNstartrain");

                ObjRain.SetActive(true);


            }
            if (Score == StartRain + 1 || Score == StartRain + 100 || Score == StartRain + 200 || Score == StartRain + 300 || Score == StartRain + 400 || Score == StartRain + 500 || Score == StartRain + 600 || Score == StartRain + 700 || Score == StartRain + 800)
            {
                Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.5f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.5f; Bird.GetComponent<Rigidbody2D>().gravityScale = 1.5f; RainDrop.SetActive(true); RainD.SetTrigger("StartRainDrop");
            }
            if (Score == EndRain || Score == EndRain + 100 || Score == EndRain + 200 || Score == EndRain + 300 || Score == EndRain + 400 || Score == EndRain + 500 || Score == EndRain + 600 || Score == EndRain + 700 || Score == EndRain + 800 || Score == EndRain + 900 || Score == EndRain + 1000)
            {
                Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
            }

            if (Score >= EndRain || Score >= EndRain + 100 || Score >= EndRain + 200 || Score >= EndRain + 300 || Score >= EndRain + 400 || Score >= EndRain + 500 || Score >= EndRain + 600 || Score >= EndRain + 700 || Score >= EndRain + 800 || Score >= EndRain + 900 || Score >= EndRain + 1000)
            {
                if (audio.isPlaying)
                {

                    audio.volume -= Time.deltaTime / 10;
                    ObjRain.GetComponent<ParticleSystem>().maxParticles -= 5;
                }
                if (audio.volume < 0.2f) audio.Stop();
                if (ObjRain.GetComponent<ParticleSystem>().maxParticles < 400)
                {
                    ObjRain.SetActive(false);
                    // MB.SetTrigger("PlainNor");
                    Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
                    Bird.GetComponent<Rigidbody2D>().gravityScale = 1.2f; RainDrop.SetActive(false); RainD.SetTrigger("RainDropNor");
                    aniHN.SetTrigger("HNnor");
                }



            }
        }
            if (ScrollSnapRect.StateInt == 1)
        {

            if (Score == StartRain || Score == StartRain + 100 || Score == StartRain + 200 || Score == StartRain + 300 || Score == StartRain + 400 || Score == StartRain + 500 || Score == StartRain + 600 || Score == StartRain + 700 || Score == StartRain + 800)
            {
                audio.volume += 0.05f;
                if (!audio.isPlaying)
                {

                    //  MB.SetTrigger("PlanNight");
                    SoundsControllerRain.PlaySound(soundsGameRain.Fade);
                    aniMT.SetTrigger("MTTT");


                }
                aniMT.SetTrigger("MTstartrain");

                ObjRain.SetActive(true);


            }
            if (Score == StartRain + 1 || Score == StartRain + 100 || Score == StartRain + 200 || Score == StartRain + 300 || Score == StartRain + 400 || Score == StartRain + 500 || Score == StartRain + 600 || Score == StartRain + 700 || Score == StartRain + 800)
            {
                Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.5f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.5f; Bird.GetComponent<Rigidbody2D>().gravityScale = 1.5f; RainDrop.SetActive(true); RainD.SetTrigger("StartRainDrop");
            }
            if (Score == EndRain || Score == EndRain + 100 || Score == EndRain + 200 || Score == EndRain + 300 || Score == EndRain + 400 || Score == EndRain + 500 || Score == EndRain + 600 || Score == EndRain + 700 || Score == EndRain + 800 || Score == EndRain + 900 || Score == EndRain + 1000)
            {
                Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
            }

            if (Score >= EndRain || Score >= EndRain + 100 || Score >= EndRain + 200 || Score >= EndRain + 300 || Score >= EndRain + 400 || Score >= EndRain + 500 || Score >= EndRain + 600 || Score >= EndRain + 700 || Score >= EndRain + 800 || Score >= EndRain + 900 || Score >= EndRain + 1000)
            {
                if (audio.isPlaying)
                {

                    audio.volume -= Time.deltaTime / 10;
                    ObjRain.GetComponent<ParticleSystem>().maxParticles -= 5;
                }
                if (audio.volume < 0.2f) audio.Stop();
                if (ObjRain.GetComponent<ParticleSystem>().maxParticles < 400)
                {
                    ObjRain.SetActive(false);
                    // MB.SetTrigger("PlainNor");
                    Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
                    Bird.GetComponent<Rigidbody2D>().gravityScale = 1.2f; RainDrop.SetActive(false); RainD.SetTrigger("RainDropNor");
                    aniMT.SetTrigger("HNnor");
                }



            } 
        

    }
        
        if (ScrollSnapRect.StateInt == 0)
        {
            if (Score == StartRain || Score == StartRain+100 || Score == StartRain + 200 || Score == StartRain + 300 || Score == StartRain + 400 || Score == StartRain + 500 || Score == StartRain + 600 || Score == StartRain + 700 || Score == StartRain + 800)
            {
                audio.volume += 0.05f;
                if (!audio.isPlaying)
                {
                 
                 //  MB.SetTrigger("PlanNight");
                    SoundsControllerRain.PlaySound(soundsGameRain.Fade);
                     aniSG.SetTrigger("SGTT");
                    
                    
                }
                aniSG.SetTrigger("SGstartrain");
               
                ObjRain.SetActive(true);
              

            }
            if (Score == StartRain + 1 || Score == StartRain + 100 || Score == StartRain + 200 || Score == StartRain + 300 || Score == StartRain + 400 || Score == StartRain + 500 || Score == StartRain + 600 || Score == StartRain + 700 || Score == StartRain + 800)
            {
                Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.8f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.8f; Bird.GetComponent<Rigidbody2D>().gravityScale = 1.8f; RainDrop.SetActive(true); RainD.SetTrigger("StartRainDrop");
            }
           
            if (Score >= EndRain || Score >= EndRain+ 100 || Score >= EndRain + 200 || Score >= EndRain + 300 || Score >= EndRain + 400 || Score >= EndRain + 500 || Score >= EndRain + 600 || Score >= EndRain + 700 || Score >= EndRain + 800 || Score >= EndRain + 900 || Score >= EndRain + 1000)
            {
                if (audio.isPlaying)
                {

                    audio.volume -= Time.deltaTime / 10;
                    ObjRain.GetComponent<ParticleSystem>().maxParticles -= 5;
                }
                    if (audio.volume < 0.2f) audio.Stop();
                    if (ObjRain.GetComponent<ParticleSystem>().maxParticles < 400) {
                    ObjRain.SetActive(false);
                  // MB.SetTrigger("PlainNor");
                    Bird1.GetComponent<Rigidbody2D>().gravityScale = 1.2f; Bird2.GetComponent<Rigidbody2D>().gravityScale = 1.2f;
                    Bird.GetComponent<Rigidbody2D>().gravityScale = 1.2f; RainDrop.SetActive(false); RainD.SetTrigger("RainDropNor");
                    aniSG.SetTrigger("SGnor");
                }
                
                

            }
        }
    }
    private void Awake()
    {
       // ScrollSnapRectC.StateIntC = 1;
        if (ScrollSnapRectC.StateIntC == 0)
        {
            Bird.SetActive(true);
        }
        if (ScrollSnapRectC.StateIntC == 1)
        {
            Bird1.SetActive(true);
        }
        if (ScrollSnapRectC.StateIntC == 2)
        {
            Bird2.SetActive(true);
        }
        if (ScrollSnapRect.StateInt == 0)
        {
            ObjSG.SetActive(true);
            ObjMT.SetActive(false);
            ObjHN.SetActive(false);
            
        }
        if (ScrollSnapRect.StateInt == 1)
        {
            ObjSG.SetActive(false);
            ObjMT.SetActive(true);
            ObjHN.SetActive(false);
        }
        if (ScrollSnapRect.StateInt == 2)
        {
            ObjSG.SetActive(false);
            ObjMT.SetActive(false);
            ObjHN.SetActive(true);
        }

    }

    int previousScore = -1;
    public Sprite[] numberSprites;
    public SpriteRenderer Units, Tens, Hundreds,HN,SG;
    public GameObject ObjRain, Bird, pac,RainDrop, Light,Bird1,Bird2,ObjSG,ObjHN,ObjMT;
    public Animator aniHN,aniSG,MB,RainD,aniMT;
    public AudioSource audio,audioall;

    
}
