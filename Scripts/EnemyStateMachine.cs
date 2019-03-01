using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : MonoBehaviour
{
    public Player player;
    public EnemyStateMachine enemy;
    public float Damaged;
    public float Attackpower;
    public Text Name;
    public Text HP;
    public Text MP;
    public bool selected;


    public enum TurnState
    {
        WAITING,
        SELECTING,
        ACTION,
        DEAD,
    };

    public TurnState currentstate;

    // Use this for initialization
    void Start()
    {
        //if we are adding level ups then this will need to change this
        player.name = "NO NAME!!!";
        Name.text = player.name;
        player.BaseHealth = 20;
        player.CurrHealth = player.BaseHealth;
        HP.text = "HP: " + player.CurrHealth;
        player.BaseMusicPoints = 10;
        player.CurrMusicPoints = player.BaseMusicPoints;
        MP.text = "MP: " + player.CurrMusicPoints;
        player.baseATK = 2;
        player.currATK = player.baseATK;
        player.baseDEF = 1;
        player.currDEF = player.baseDEF;
        selected = false;

        currentstate = TurnState.SELECTING;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentstate)
        {
            case (TurnState.WAITING):
                if (enemy.currentstate == EnemyStateMachine.TurnState.WAITING)
                {
                    currentstate = TurnState.SELECTING;
                }
                break;

            case (TurnState.SELECTING):

                if (selected)
                {
                    currentstate = TurnState.ACTION;
                    selected = false;

                }
                break;
            case (TurnState.ACTION):
                //update the stats of the enemy and the player 
                enemy.enemy._CurrHealth -= Attackpower;
                MP.text = MP.text = "MP: " + player.CurrMusicPoints;
                if (enemy.enemy._CurrHealth < 0)
                {
                    enemy.enemy._CurrHealth = 0;
                }
                enemy.HP.text = "HP: " + enemy.enemy._CurrHealth;
                currentstate = TurnState.WAITING;
                if (enemy.enemy._CurrHealth <= 0)
                {
                    enemy.currentstate = EnemyStateMachine.TurnState.DEAD;
                }

                break;
            case (TurnState.DEAD):

                break;


        }
    }


    public void ChooseAction(Button button)
    {
        Debug.Log("You have clicked the" + button.name);
        switch (player.weapontype)
        {
            case Player.WeaponType.UNARMED:
                // attack with a light attack 
                if (button.name == "Whistle")
                {
                    Attackpower = Random.Range(1, 2);
                    selected = true;
                }
                // attack with a heavy attack
                else if (button.name == "Sing")
                {
                    if (player.CurrMusicPoints - 3.0 >= 0.0)
                    {
                        player.CurrMusicPoints = player.CurrMusicPoints - 3.0f;
                        Attackpower = Random.Range(2, 4);
                        selected = true;
                    }
                    else
                    {
                        selected = false;
                    }
                }
                //defend yourself
                else if (button.name == "DEF")
                {
                    Attackpower = 0;
                    selected = true;


                }
                else
                {
                    selected = false;
                }
                break;
            case Player.WeaponType.PERCUSSION:

                break;
            case Player.WeaponType.STRING:
                break;
            case Player.WeaponType.WIND:
                break;
        }
    }
}





