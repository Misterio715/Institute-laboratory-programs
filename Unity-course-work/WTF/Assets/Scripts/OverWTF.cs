using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWTF : MonoBehaviour
{
    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnAgain()
    {
        SceneManager.LoadScene(1);
    }
}
