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
    public bool isDefending;
    public bool buffed;
    public float echoDmg;
    public bool echo;
    public float buffMod;
    public List<GameObject> menu;

    public AudioClip sound;
    private Button _button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }



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
    	WINDPLAY,
    	WINDTUNE,
    	CHARM,
    	STRINGPLAY,
    	STRINGTUNE,
    	CRITICIZE,
    	DRUMPLAY,
    	FORTISSISSIMO,
    	ECHO,
    }

    public TurnState currentstate;
    public bool isBusy;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        //if we are adding level ups then this will need to change this
        player.name = "Conductor";
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
        isDefending = false;
        buffed = false;
        buffMod = 1.5f;
        echoDmg = 0.0f;
        echo = false;
        //hello world
        currentstate = TurnState.SELECTING;
        anim = GetComponent<Animator>();

        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
       _button.onClick.AddListener(() => ChooseAction(_button));

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
                //update player health
                player.CurrHealth -= Damaged;
                if (player.CurrHealth - Damaged >= player.BaseHealth)
                {
                    player.CurrHealth = player.BaseHealth;
                }
                //Apply buff
                if (buffed) {
                	if (Attackpower % 2 == 0) {
                		Attackpower = Attackpower * buffMod;
                	} else {
                		Attackpower = Attackpower * buffMod;
                		Attackpower += (buffMod - 1);
                	}
                	if (Attackpower > 0) {
                		buffed = false;
                	}
                }

                if (echo != true && echoDmg > 0) {
                	enemy.enemy._CurrHealth -= echoDmg;
                	echoDmg = 0.0f;
                } else if (echo == true) {
                	echo = false;
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
        PlaySound();
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
            PlaySound();
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
    			case (AnimState.WINDPLAY):
    				anim.Play("PlayerWindPlay");
    				break;
    			case (AnimState.WINDTUNE):
    				anim.Play("PlayerWindTune");
    				break;
    			case (AnimState.CHARM):
    				anim.Play("PlayerCharm");
    				break;
    			case (AnimState.STRINGPLAY):
    				anim.Play("PlayerStringPlay");
    				break;
    			case (AnimState.STRINGTUNE):
    				anim.Play("PlayerStringTune");
    				break;
    			case (AnimState.CRITICIZE):
    				anim.Play("PlayerCriticize");
    				break;
    			case (AnimState.DRUMPLAY):
    				anim.Play("PlayerDrumPlay");
    				break;
    			case (AnimState.FORTISSISSIMO):
    				anim.Play("PlayerFortississimo");
    				break;
    			case (AnimState.ECHO):
    				anim.Play("PlayerEcho");
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
        if (button.name == "WPlay")
        {
            Attackpower = Random.Range(1, 1);
            selected = true;
            handleAnim(AnimState.WINDPLAY);
        }
        // heal
        else if (button.name == "Repair")
        {
            if (player.CurrMusicPoints - 2.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 2.0f;
                Damaged = -Random.Range(2, 3);
                selected = true;
                handleAnim(AnimState.WINDTUNE);
            }
        }
        // steal enemy mp
        else if (button.name == "Charm")
        {
            
            MPpool = Random.Range(2, 4);
            float enemyMP = enemy.enemy._CurrMusicPoints - MPpool;
            if (enemyMP >= 0)
            {
                selected = true;
                handleAnim(AnimState.CHARM);
            }else if (enemy.enemy._CurrMusicPoints > 0 && MPpool >= -3)
            {
                MPpool = enemy.enemy._CurrMusicPoints;
                selected = true;
                handleAnim(AnimState.CHARM);
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
        if (button.name == "SPlay")
        {
            if (player.CurrMusicPoints - 3.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 3.0f;
                Attackpower = Random.Range(2, 4);
                selected = true;
                handleAnim(AnimState.STRINGPLAY);
            }
            else
            {
                selected = false;
            }
        }
        // buff your next attack
        else if (button.name == "STune")
        {
        	if (player.CurrMusicPoints - 3.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 3.0f;
                buffed = true;
        		Attackpower = 0;
            	selected = true;
            	handleAnim(AnimState.STRINGTUNE);
            }
            else
            {
                selected = false;
            }
        }
        // give the enemy a miss chance on their next attack
        else if (button.name == "Criticize")
        {
        	if (player.CurrMusicPoints - 1.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 1.0f;
                enemy.debuffed = true;
        		Attackpower = 0;
            	selected = true;
            	handleAnim(AnimState.CRITICIZE);
            }
            else
            {
                selected = false;
            }
        }
    }

    //Action for Percussions
    void PercussionAction(Button button)
    {
        // attack with a very heavy attack 
        if (button.name == "PPlay")
        {
            if (player.CurrMusicPoints - 5.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 5.0f;
                Attackpower = Random.Range(5, 8);
                selected = true;
                handleAnim(AnimState.DRUMPLAY);
            }
            else
            {
                selected = false;
            }
        }
        // attack with a chance to stagger
        else if (button.name == "Fortississimo")
        {
        	if (player.CurrMusicPoints - 3.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 3.0f;
                enemy.debuffed = true;
                Attackpower = Random.Range(2,3);
            	selected = true;
            	handleAnim(AnimState.FORTISSISSIMO);
            }
        	else
        	{
        		selected = false;
        	}
        }
        // hit the enemy with an attack that echoes and does damage again next turn
        else if (button.name == "Echo")
        {
        	if (player.CurrMusicPoints - 1.0 >= 0.0)
            {
                player.CurrMusicPoints = player.CurrMusicPoints - 1.0f;
                Attackpower = Random.Range(1,2);
                echoDmg = Attackpower;
                echo = true;
            	selected = true;
            	handleAnim(AnimState.ECHO);
            }
        	else
        	{
        		selected = false;
        	}
        }
    }

//defend from attack
    public void Action_DEF(Button button)
    {
        if (button.name == "DEF")
        {
            Attackpower = 0;
            selected = true;
            isDefending = true;
            handleAnim(AnimState.DEF);
        }
        else
        {
            selected = false;
        }
    }

    void PlaySound()
    {
        if (sound != null)
        { 
            source.PlayOneShot(sound);
        }

    }
}




