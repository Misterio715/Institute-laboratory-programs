using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text Text2;

    private void Update()
    {
        Text2.text = GameManager.Singleton.GetScore().ToString();
    }
}
