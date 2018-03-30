using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineSql : MonoBehaviour {
    private static OnlineSql sQL;

    public static OnlineSql SQL
    {
        get
        {
            if (sQL == null)
            {
                sQL = new OnlineSql();
            }
            return OnlineSql.sQL;

        }
        private set { OnlineSql.sQL = value; }
    }

    private OnlineSql() { }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void FuckAdd(string name, int score)
    {

        string post_url = "http://crazybird.uphero.com/Game/Unity2D&3D/FlappyBirdK14/AddS.php?" + "name=" + name + "&score=" + score;

        WWW hs_post = new WWW(post_url);
        Debug.Log(post_url);
        Debug.Log(hs_post.error);
        if (hs_post.error != null)
        {
            Debug.Log("There was an error posting the high score: " + hs_post.error);
        }
        else
        {
            Debug.Log("Posted the high score: ");
        }
    }
}
