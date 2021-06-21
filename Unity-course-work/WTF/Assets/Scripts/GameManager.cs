using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager singleton;
    private int score;

    public static GameManager Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameManager();
            }

            return singleton;
        }
    }

    public void SetScore(int s)
    {
        score = s;
    }

    public int GetScore()
    {
        return score;
    }
}
