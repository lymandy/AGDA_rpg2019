using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    public Enemy enemy;
    public PlayerStateMachine player;
    public float Damage;
    public float Attackpower;
    public Text Name;
    public Text HP;


    public enum TurnState
    {
        WAITING,
        SELECTING,
        ACTION,
        DEAD,
    };

    public enum AttackType
    {
        HEAVYATTACK,
        LIGHTATTACK,
        HEAL,
        DEFENSE,
    }

    public TurnState currentstate;
    // Use this for initialization
    void Start()
    {
        currentstate = TurnState.WAITING;
        Damage = 0.0f;
        Attackpower = 0.0f;
        SetEnemyType();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentstate)
        {
            case (TurnState.WAITING):
                if (player.currentstate.Equals( PlayerStateMachine.TurnState.WAITING))
                {
                    currentstate = TurnState.SELECTING;
                }
                break;

            case (TurnState.SELECTING):
                ChooseAction();

                currentstate = TurnState.ACTION;
                break;
            case (TurnState.ACTION):
                
                player.player.CurrHealth = player.player.CurrHealth - Attackpower;
                if (player.player.CurrHealth < 0)
                {
                    player.player.CurrHealth = 0;
                }
                player.HP.text = "HP: " + player.player.CurrHealth;
                currentstate = TurnState.WAITING;
                if (player.player.CurrHealth <= 0)
                {
                    player.currentstate = PlayerStateMachine.TurnState.DEAD;
                }

   
                break;
            case (TurnState.DEAD):

                break;


        }
    }

    void SetEnemyType()
    {
        var enemytype = enemy._Etype;
        switch (enemytype)
        {
            case (Enemy.Type.DRUM):
                enemy._name = "The Drums";
                Name.text = enemy._name;
                enemy._BaseHealth = 20;
                enemy._CurrHealth = enemy._BaseHealth;
                HP.text = "HP: " + enemy._CurrHealth;
                enemy._BaseMusicPoints = 10;
                enemy._CurrMusicPoints = enemy._BaseMusicPoints;
                enemy._baseATK = 2;
                enemy._currATK = enemy._baseATK;
                enemy._baseDEF = 1;
                enemy._currDEF = enemy._baseDEF;

                break;
                //when more types are added then expand this swich 
        }
    }

    void ChooseAction()
    {
        var enemytype = enemy._Etype;
        switch (enemytype)
        {
            case (Enemy.Type.DRUM):
                DrumAction();

                break;
                //when more types are added then expand this swich 
        }
    }
    //Actions that the Drum can do
    void DrumAction()
    {
        int randomNum = Random.Range(0, 2);
        switch (randomNum)
        {
            case (0):
                //Light Attack 
                Attackpower = Random.Range(1, 2);
                break;
            case (1):
                //Heavy Attack 
                if (enemy._CurrMusicPoints - 3.0f >= 0)
                {
                  //  enemy._CurrMusicPoints = enemy._CurrMusicPoints - 3.0f;
                    Attackpower = Random.Range(2, 4);
                }
                else
                {
                    DrumAction();
                }
                break;
            case (2):
                //Defense
                Attackpower = 0.0f;
                break;
        }
    }
}

