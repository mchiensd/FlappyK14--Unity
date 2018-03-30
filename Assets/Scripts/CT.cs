using UnityEngine;
using System.Collections;

public class CT : MonoBehaviour
{
    public void LoadGamePlay()
    {
        Application.LoadLevel("GamePlay");
    }

    public void LoadHighScoreScene()
    {
        Application.LoadLevel("highscore");
    }

    public void LoadMenuScene()
    {
        Application.LoadLevel("main");
    }
}