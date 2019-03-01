using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Enemy {

    public string _name;
    public enum Type
    {
        DRUM,
    }
    public Type _Etype;

    public float _BaseHealth;
    public float _CurrHealth;

    public float _BaseMusicPoints;
    public float _CurrMusicPoints;

    public float _baseATK;
    public float _currATK;
    public float _baseDEF;
    public float _currDEF;

 

}
