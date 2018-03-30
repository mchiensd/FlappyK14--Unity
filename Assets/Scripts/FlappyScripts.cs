using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class FlappyScripts : MonoBehaviour
{
    
    public GameObject p1, d1, p2, d2, p3, d3,p4,d4,p5,d5,p6,d6,p7,d7,p8,d8,p9,d9,p10,d10,Textname,GameOver,Hc,Hbc,up;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    public GameObject IntroGUI, DeathGUI,FS,B,high,scoreoffend,scorebad,menu;
    public Collider2D restartButtonGameCollider;
    public float VelocityPerJump = 3;
    public float XSpeed = 1;
    public Animator myani,cam;
    List<Scores> highscore;
    void Start()
    {
        Time.timeScale = 1.5f;
        GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        highscore = new List<Scores>();
        highscore = HC._instance.GetHighScore();
        ScoreManage.Score = 0;
        BackgroundMoving.MoveSpeed = 0.5f;
        GrassMoving.MoveSpeed = 2;
        TreeMoving.movey = 0f;      
        TreeMoving.MoveSpeed  = 2;
        FS.SetActive(false);
        
    }

    FlappyYAxisTravelState flappyYAxisTravelState;

    enum FlappyYAxisTravelState
    {
        GoingUp, GoingDown
    }

    Vector3 birdRotation = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        
        // khi nhấn nút quay lại thỳ out game
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (GameStateManager.GameState == GameState.Intro)
        {
             
            transform.position += new Vector3(0, 0, 0);
            if (WasTouchedOrClicked())
            {
                GameStateManager.GameState = GameState.Playing;    
                BoostOnYAxis();                       
                FS.SetActive(true);           
                IntroGUI.SetActive(false);
               ScoreManage.Score = 0;
            }
            
        }
        
        else if (GameStateManager.GameState == GameState.Playing)
        {
           
            if (WasTouchedOrClicked())
            {
               myani.SetTrigger("BirdTap");
                BoostOnYAxis();
            }

        }

        else if (GameStateManager.GameState == GameState.Dead)
        {           
            Vector2 contactPoint = Vector2.zero;
            if (Input.touchCount > 0)
                contactPoint = Input.touches[0].position;
            if (Input.GetMouseButtonDown(0))
                contactPoint = Input.mousePosition;         
        }

    }


    void FixedUpdate()
    {
        
        //Code cho chim nhãy cà nhãy ở phần intro kakakaka :D
        if (GameStateManager.GameState == GameState.Intro)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < -1) //Khi nó rơi thỳ boot lên
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, GetComponent<Rigidbody2D>().mass * 6190 * Time.deltaTime)); 
                                                                                                                                
        }
        else if (GameStateManager.GameState == GameState.Playing || GameStateManager.GameState == GameState.Dead)
        {
            FixFlappyRotation();
            
        }
        LoadHighScore();
    }

    bool WasTouchedOrClicked()
    {
        
        if(Input.GetMouseButtonDown(0))
            return true;
        else
        {
            if (Input.GetButtonDown("Jump")) return true;
        }
            return false;
    }


    void BoostOnYAxis()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, VelocityPerJump);
        SoundsController.PlaySound(soundsGame.wing);
    }



   // code chim rơi và xoay 45 độ
    private void FixFlappyRotation()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > 0) flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;

        float degreesToAdd = 0;

        switch (flappyYAxisTravelState)
        {
            case FlappyYAxisTravelState.GoingUp:
                degreesToAdd = 6 * RotateUpSpeed;
                break;
            case FlappyYAxisTravelState.GoingDown:
                degreesToAdd = -2 * RotateDownSpeed;
                break;
            default:
                break;
        }
      
        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -90, 45));
        transform.eulerAngles = birdRotation;
    }

 //phân biệt ống để ghi điểm hay chết
    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Point") // lấy những colider đc phân biệt bởi tag
            {
                SoundsController.PlaySound(soundsGame.point);
                ScoreManage.Score++;
                   TreeMoving.MoveSpeed += Time.deltaTime;
               
                
            }
           

        }
    }

   
    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "FS")  // lấy những colider đc phân biệt bởi tag
            {
                FlappyDies();
            }
        }
    }

    void FlappyDies()
    {
       
        scoreoffend.SetActive(false);
        SoundsController.PlaySound(soundsGame.die);
        SoundsController.PlaySound(soundsGame.hit);
        TreeMoving.MoveSpeed = 0;
        TreeMoving.movey = 0;
        GrassMoving.MoveSpeed = 0;
        BackgroundMoving.MoveSpeed = 0;
        GameStateManager.GameState = GameState.Dead;
         myani.SetTrigger("BirdDie");
        DeathGUI.SetActive(true);  
        menu.SetActive(true);
        cam.SetTrigger("CamDie");
        GetComponent<Rigidbody2D>().gravityScale = 1.2f;
        XuliHighScore();

    }

    

    void UploadHC()
    {
        OnlineSql.SQL.FuckAdd(Button.N, ScoreManage.Score);
    }
    void XuliHighScore()
    {
     
       
        {
            if (ScoreManage.Score > int.Parse( d10.GetComponent<Text>().text))
            {
                HC._instance.SaveHighScore(Button.N, ScoreManage.Score); highscore = HC._instance.GetHighScore();              
                SoundsController.PlaySound(soundsGame.cheer);
                high.SetActive(true);
                if (ScoreManage.Score >= int.Parse(d1.GetComponent<Text>().text))
                {
                    up.SetActive(true);
                    HighScore.ScoreBest = ScoreManage.Score;
                    Hbc.SetActive(true); 
                    {
                        UploadHC();
                        Destroy(up, 2);


                    }
                    
                }
                else
                {
                    
                    HighScore.ScoreBest = highscore[0].score;
                    Hc.SetActive(true);
                }
                
            }
            else 
            {
                if (ScoreManage.Score == 0 && checkreset.checkrss == 1)
                {
                    GameOver.SetActive(true);
                    HighScore.ScoreBest = 0;
                    Debug.Log("RESET"); checkreset.checkrss = 0;

                }
                else
                {
                    GameOver.SetActive(true);
                    HighScore.ScoreBest = highscore[0].score;
                }
            }
            
      
          
        }

    }
   void LoadHighScore()
    {
        highscore = HC._instance.GetHighScore();
        p1.GetComponent<Text>().text = "" + highscore[0].name.ToString();
        d1.GetComponent<Text>().text = "" + highscore[0].score.ToString();
        p2.GetComponent<Text>().text = "" + highscore[1].name.ToString();
        d2.GetComponent<Text>().text = "" + highscore[1].score.ToString();
        p3.GetComponent<Text>().text = "" + highscore[2].name.ToString();
        d3.GetComponent<Text>().text = "" + highscore[2].score.ToString();
        p4.GetComponent<Text>().text = "" + highscore[3].name.ToString();
        d4.GetComponent<Text>().text = "" + highscore[3].score.ToString();
        p5.GetComponent<Text>().text = "" + highscore[4].name.ToString();
        d5.GetComponent<Text>().text = "" + highscore[4].score.ToString();
        p6.GetComponent<Text>().text = "" + highscore[5].name.ToString();
        d6.GetComponent<Text>().text = "" + highscore[5].score.ToString();
        p7.GetComponent<Text>().text = "" + highscore[6].name.ToString();
        d7.GetComponent<Text>().text = "" + highscore[6].score.ToString();
        p8.GetComponent<Text>().text = "" + highscore[7].name.ToString();
        d8.GetComponent<Text>().text = "" + highscore[7].score.ToString();
        p9.GetComponent<Text>().text = "" + highscore[8].name.ToString();
        d9.GetComponent<Text>().text = "" + highscore[8].score.ToString();
        p10.GetComponent<Text>().text = "" + highscore[9].name.ToString();
        d10.GetComponent<Text>().text = "" + highscore[9].score.ToString();
        


    }          
       
            
    
    private void Awake()
    {
        IntroGUI.SetActive(true);
        DeathGUI.SetActive(false);
        
    }

}
