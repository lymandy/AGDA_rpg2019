using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStateMachine : MonoBehaviour
{
    public Player player;
    public EnemyStateMachine enemy;
    public float Damaged;
    public float Attackpower;
    public float MPpool;
    public Text Name;
    public Text HP;
    public Text MP;
    public bool selected;
    public List<GameObject> menu;


    public enum TurnState
    {
        WAITING,
        SELECTING,
        ACTION,
        DEAD,
    };

    public TurnState currentstate;
    private Animator anim;

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
        //hello world
        currentstate = TurnState.SELECTING;
        anim = GetComponent<Animator>();

    }

    // Update is called and checking the state of the player 
    void Update()
    {
        switch (currentstate)
        {
            case (TurnState.WAITING):
                if (enemy.currentstate.Equals(EnemyStateMachine.TurnState.WAITING))
                {
                    currentstate = TurnState.SELECTING;
                }
                break;

            case (TurnState.SELECTING):

                if (selected)
                {
                    anim.Play("WhistleAnim");
                    currentstate = TurnState.ACTION;
                    selected = false;
                }
                break;
            case (TurnState.ACTION):
                //update the stats of the enemy and the player 
                //update player health
                player.CurrHealth -= Damaged;
                if (player.CurrHealth - Damaged >= player.BaseHealth)
                {
                    player.CurrHealth = player.BaseHealth;
                }

                //update enemy health
                enemy.enemy._CurrHealth -= Attackpower;
               
                if (enemy.enemy._CurrHealth < 0)
                {
                    enemy.enemy._CurrHealth = 0;
                }
                
               
                if (enemy.enemy._CurrHealth <= 0)
                {
                    enemy.currentstate = EnemyStateMachine.TurnState.DEAD;
                }
                //update enemy and player mp
                enemy.enemy._CurrMusicPoints -= MPpool;
                player.CurrMusicPoints += MPpool;
                if (player.CurrMusicPoints + MPpool > player.BaseMusicPoints)
                {
                    player.CurrMusicPoints = player.BaseMusicPoints;
                }
                enemy.HP.text = "HP: " + enemy.enemy._CurrHealth;
                MP.text = MP.text = "MP: " + player.CurrMusicPoints;
                enemy.HP.text = "HP: " + enemy.enemy._CurrHealth;
                Attackpower = 0;
                Damaged = 0;
                MPpool = 0;
                currentstate = TurnState.WAITING;
                break;
            case (TurnState.DEAD):
                //go to game over scene
                SceneManager.LoadScene(4);
                break;


        }
    }
    //Depending on weapon type, different action menu will appear
    public void ShowAction()
    {
        
        switch (player.weapontype)
        {
            case Player.WeaponType.WIND:
                menu[1].SetActive(true);
                break;
            case Player.WeaponType.PERCUSSION:
                menu[3].SetActive(true);
                break;
            case Player.WeaponType.STRING:
                menu[2].SetActive(true);
                break;
            case Player.WeaponType.UNARMED:
                menu[0].SetActive(true);
                break;
        }
    }

    //Close all action menus 
    public void CloseActionMenu()
    {
        for (int i = 0; i < menu.Count; i++)
        {
            if (menu[i].activeInHierarchy == true)
            {
                menu[i].SetActive(false);
            }
        }
    }
    
    //Choose Action once Act is selected ( this depends on what instrument the player is using)
    public void ChooseAction(Button button)
    {
        Debug.Log("You have clicked the" + button.name);
        switch (player.weapontype)
        {
            case Player.WeaponType.UNARMED:
                UnarmedAction(button);
                break;
            case Player.WeaponType.PERCUSSION:
                PercussionAction(button);
                break;
            case Player.WeaponType.STRING:
                StringAction(button);
                break;
            case Player.WeaponType.WIND:
                WindAction(button);
                break;
        }
        if (selected)
        {
            CloseActionMenu();
        }
    }

    //Action for Unarmed 
    void UnarmedAction(Button button)
    {
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
    }

    //Action for Wind 
    void WindAction(Button button)
    {
        // attack with a light attack 
        if (button.name == "Play")
        {
            Attackpower = Random.Range(1, 1);
            selected = true;
        }
        // attack with a heavy attack
        else if (button.name == "Tune")
        {
            if (player.CurrMusicPoints - 2.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 2.0f;
                Damaged = -Random.Range(2, 3);

                selected = true;
            }
        }
        else if (button.name == "Charm")
        {
            
            MPpool = Random.Range(2, 4);
            float enemyMP = enemy.enemy._CurrMusicPoints - MPpool;
            if (enemyMP >= 0)
            {
                selected = true;
            }else if (enemy.enemy._CurrMusicPoints > 0 && MPpool >= -3)
            {
                MPpool = enemy.enemy._CurrMusicPoints;
                selected = true;
            }else{
                MPpool = 0;
                selected = false;
            }
            
            
        }

    }

    //Action for String
    void StringAction(Button button)
    {
        // attack with a light attack 
        if (button.name == "Play")
        {
            if (player.CurrMusicPoints - 3.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - .0f;
                Attackpower = Random.Range(2, 4);
                selected = true;
            }
            else
            {
                selected = false;
            }
        }
        // attack with a heavy attack
        else if (button.name == "Tune")
        {
            selected = true;
        }
        else if (button.name == "Critize")
        {
            selected = true;
        }
    }

    //Action for Percussions
    void PercussionAction(Button button)
    {
        // attack with a light attack 
        if (button.name == "Play")
        {
            if (player.CurrMusicPoints - 5.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 5.0f;
                Attackpower = Random.Range(5, 8);
                selected = true;
            }
            else
            {
                selected = false;
            }
        }
        // attack with a heavy attack
        else if (button.name == "2")
        {
            selected = true;
        }
        else if (button.name == "3")
        {

            selected = true;
        }
    }

//defend from attack
    public void Action_DEF(Button button)
    {
        if (button.name == "DEF")
        {
            Attackpower = 0;
            selected = true;
        }
        else
        {
            selected = false;
        }
    }
}




