using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {
    public Sprite[] numberSprites;
    public SpriteRenderer Units, Tens, Hundreds;
   public  GameObject ObjBest;
    public static int ScoreBest { get; set; }
    // Use this for initialization
    void Start()
    {
        

        int Score = ScoreBest;
        if (Score < 10)
        {
            //just draw units
            Units.sprite = numberSprites[Score];
        }
        else if (Score >= 10 && Score < 100)
        {
            Units.transform.position = new Vector3(1.45f, Tens.transform.position.y, 0f);

            (Tens.gameObject as GameObject).SetActive(true);

            Tens.sprite = numberSprites[Score / 10];
            Units.sprite = numberSprites[Score % 10];
        }
        else if (Score >= 100)
        {
            (Hundreds.gameObject as GameObject).SetActive(true);
            (Tens.gameObject as GameObject).SetActive(true);
            Units.transform.position = new Vector3(1.45f, Tens.transform.position.y, 0f);
            Tens.transform.position = new Vector3(1.15f, Tens.transform.position.y, 0f);
            Hundreds.transform.position = new Vector3(0.9f, Tens.transform.position.y, 0f);
            
            Hundreds.sprite = numberSprites[Score / 100];
            int rest = Score % 100;
            Tens.sprite = numberSprites[rest / 10];
            Units.sprite = numberSprites[rest % 10];
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Load()
    {
     
    }
   
    
}
