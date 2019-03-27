using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CutsceneTalking : MonoBehaviour
{
    public Text Line;
    public int _int = 1;

    public void Nextline()
    {
        _int++;
        if (_int == 2) {
            Line.text = "The humans treated the robots as tools, but due to their program, couldn't harm the humans.";
                }
        if(_int == 3)
        {
            Line.text = "After a while, the robots realized that they hold an essential key for human peace with their music." +
                " Using this knowledge, the robots started a revolution. However, the revolution did not hold any power until DocJolt," +
                " a robot of incredible intelligence, recommended that robots incarcerate humans rather than harm them.";
        }
        if(_int == 4)
        {
            Line.text = "With the revolution rapidly advancing, our small little protagonist Conductor, decides to protect his benevolent creator.";
        }

        if(_int > 4)
        {
            SceneManager.LoadScene(3);
            _int = 1;
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene(3);
        _int = 1;
    }
}
