using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public PlayerStateMachine player;
    public Text E1text;
    public Text E2text;
    public Text E3text;
	// Use this for initialization
	public void SetEquip () {
        switch (player.player.weapontype)
        {
            case Player.WeaponType.UNARMED:
                E1text.text = "Percussion";
                E2text.text = "String";
                E3text.text = "Wind";

                break;
            case Player.WeaponType.STRING:
                E1text.text = "Percussion";
                E2text.text = "Unarmored";
                E3text.text = "Wind";
                break;
            case Player.WeaponType.WIND:
                E3text.text = "Unarmed";
                E2text.text = "Percussion";
                E1text.text = "String";
                break;
            case Player.WeaponType.PERCUSSION:
                E1text.text = "Unarmored";
                E2text.text = "String";
                E3text.text = "Wind";
                break;
        }
	}

    public void ChangeEquip(Button button)
    {
        Debug.Log("this button is " + button.name);
        switch (button.name)
        {
            case "Equipment 1":
                ChangeWeaponType(E1text);
                break;
            case "Equipment 2":
                ChangeWeaponType(E2text);
                break;
            case "Equipment 3":
                ChangeWeaponType(E3text);
                break;
        }
        SetEquip();
    }
	
    void ChangeWeaponType(Text text)
    {
        Debug.Log("the name is" + text.text);
        if (text.text == "Percussion")
        {
            player.player.weapontype = Player.WeaponType.PERCUSSION;
        }
        if (text.text == "Wind")
        {
            player.player.weapontype = Player.WeaponType.WIND;
        }
        if (text.text == "String")
        {
            player.player.weapontype = Player.WeaponType.STRING;
 
        }
        if (text.text == "Unarmed")
        {
            player.player.weapontype = Player.WeaponType.UNARMED;
        }
    }

}
