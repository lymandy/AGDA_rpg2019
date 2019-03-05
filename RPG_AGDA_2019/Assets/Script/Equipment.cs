using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public PlayerStateMachine playermachine;
    public Text E1text;
    public Text E2text;
    public Text E3text;
	//udepending on what weapon the player is using
	public void SetEquip () {
        switch (playermachine.player.weapontype)
        {
            case Player.WeaponType.UNARMED:
                E1text.text = "Percussion";
                E2text.text = "String";
                E3text.text = "Wind";

                break;
            case Player.WeaponType.STRING:
                E1text.text = "Percussion";
                E2text.text = "Unarmed";
                E3text.text = "Wind";
                break;
            case Player.WeaponType.WIND:
                E3text.text = "Unarmed";
                E2text.text = "Percussion";
                E1text.text = "String";
                break;
            case Player.WeaponType.PERCUSSION:
                E1text.text = "Unarmed";
                E2text.text = "String";
                E3text.text = "Wind";
                break;
        }
	}

    public void ChangeEquip(Button button)
    {
        SetEquip();
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
            playermachine.player.weapontype = Player.WeaponType.PERCUSSION;
        }
        if (text.text == "Wind")
        {
            playermachine.player.weapontype = Player.WeaponType.WIND;
        }
        if (text.text == "String")
        {
            playermachine.player.weapontype = Player.WeaponType.STRING;
 
        }
        if (text.text == "Unarmed")
        {
            playermachine.player.weapontype = Player.WeaponType.UNARMED;
        }
    }

}
