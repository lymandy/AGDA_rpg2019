﻿using System.Collections;
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

    public enum AnimState
    {
    	HEAL,
    	DEF,
    	WHISTLE,
    	SING,
    }

    public TurnState currentstate;
    public bool isBusy;
    public Animator anim;

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
                	if (!anim.GetBool("busy")) {
                		currentstate = TurnState.ACTION;
                		selected = false;
                	} else {
                		selected = false;
                	}
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
                //go to game over scene
                SceneManager.LoadScene(4);
                break;


        }
    }

    public void setBusy(bool b) {
    	isBusy = b;
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

    //Play animation
    void handleAnim(AnimState s) 
    {
    	if (!anim.GetBool("busy")) {
    		switch(s)
    		{
    			case (AnimState.DEF):
    				anim.Play("PlayerDef 0");
    				break;
    			case (AnimState.WHISTLE):
    				anim.Play("PlayerWhistle");
    				break;
    			case (AnimState.SING):
    				anim.Play("PlayerSing");
    				break;
    		}
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
            handleAnim(AnimState.WHISTLE);
        }
        // attack with a heavy attack
        else if (button.name == "Sing")
        {
            if (player.CurrMusicPoints - 3.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 3.0f;
                Attackpower = Random.Range(2, 4);
                selected = true;
                handleAnim(AnimState.SING);
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
            Attackpower = Random.Range(1, 2);
            selected = true;
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

    //Action for String
    void StringAction(Button button)
    {
        // attack with a light attack 
        if (button.name == "Play")
        {
            Attackpower = Random.Range(1, 2);
            selected = true;
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

    //Action for Percussions
    void PercussionAction(Button button)
    {
        // attack with a light attack 
        if (button.name == "Play")
        {
            Attackpower = Random.Range(1, 2);
            selected = true;
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
            handleAnim(AnimState.DEF);
        }
        else
        {
            selected = false;
        }
    }
}




