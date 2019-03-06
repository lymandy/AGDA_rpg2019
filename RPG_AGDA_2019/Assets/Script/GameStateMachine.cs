using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{

    public void BackToMainMenu()
    {

        SceneManager.LoadScene(2);
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void BackToLevelSelect()
    {

        SceneManager.LoadScene(5);
    }

}
