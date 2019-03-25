using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{

    public void BackToMainMenu()
    {

        SceneManager.LoadScene(0);
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void OptionMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }


    public void BackToLevelSelect()
    {

        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
