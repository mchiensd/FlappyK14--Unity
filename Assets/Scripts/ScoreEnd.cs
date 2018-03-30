using UnityEngine;
using System.Collections;

public class ScoreEnd : MonoBehaviour
{


    public int Score = ScoreManage.Score;
    

    // Use this for initialization


    void Start()
    {
       
        (Tens.gameObject as GameObject).SetActive(false);
        (Hundreds.gameObject as GameObject).SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        Score = ScoreManage.Score;

        if (previousScore != Score) //save perf from non needed calculations
        {
            if (Score < 10)
            {
                //just draw units
                Units.sprite = numberSprites[Score];
            }
            else if (Score >= 10 && Score < 100)
            {
                Units.transform.position = new Vector3(0.2f, Tens.transform.position.y, 0f);
                Tens.transform.position = new Vector3(-0.3f, Tens.transform.position.y, 0f);
                (Tens.gameObject as GameObject).SetActive(true);

                Tens.sprite = numberSprites[Score / 10];
                Units.sprite = numberSprites[Score % 10];
            }
            else if (Score >= 100)
            {
                (Hundreds.gameObject as GameObject).SetActive(true);
                (Tens.gameObject as GameObject).SetActive(true);
                Units.transform.position = new Vector3(0.6f, Tens.transform.position.y, 0f);
                Tens.transform.position = new Vector3(0f, Tens.transform.position.y, 0f);
                Hundreds.transform.position = new Vector3(-0.6f, Tens.transform.position.y, 0f);
                Hundreds.sprite = numberSprites[Score / 100];
                int rest = Score % 100;
                Tens.sprite = numberSprites[rest / 10];
                Units.sprite = numberSprites[rest % 10];
            }
           
        }

    }
  
    private void Awake()
    {
        //   Debug.Log("a: " + Random.Range(0, 10));
    }

    int previousScore = -1;
    public Sprite[] numberSprites;
    public SpriteRenderer Units, Tens, Hundreds;
}
