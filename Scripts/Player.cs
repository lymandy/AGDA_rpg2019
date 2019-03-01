using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable
    ]
public class Player {
    public string name;
    public float BaseHealth;
    public float CurrHealth;

    public float BaseMusicPoints;
    public float CurrMusicPoints;

    public float Defense;

    public enum WeaponType
    {
        UNARMED,//basis attack 
        STRING,//debuff
        WIND,//cleric/buff
        PERCUSSION,//damage 
    };

    public WeaponType weapontype;
    

    //public float dexterity;
    //publi float hitpoints;
}

