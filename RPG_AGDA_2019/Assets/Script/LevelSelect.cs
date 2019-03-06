using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void BackToMainMenu()
    {

        SceneManager.LoadScene(2);
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene(3);
    }
}
