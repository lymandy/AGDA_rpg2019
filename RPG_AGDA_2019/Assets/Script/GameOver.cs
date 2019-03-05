using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void Retry()
    {
  
        SceneManager.LoadScene(3);
    }

    public void BackToLevelSelect()
    {

        SceneManager.LoadScene(5);
    }

    public void BackToMainMenu()
    {
    
        SceneManager.LoadScene(2);
    }

}
