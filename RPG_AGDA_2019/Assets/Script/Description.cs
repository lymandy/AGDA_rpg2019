using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _Boxtext;
    public Text _Description;
    // Start is called before the first frame update
    void Start()
    {
      //  _Boxtext = GameObject.Find("Information");
      //  _Description = _Boxtext.transform.Find("Text").gameObject.GetComponentInChildren<Text>();
        _Boxtext.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string name = this.name;

        switch (name)
        {
            case ("Act"):
                _Description.text = "Show list of actions";
                break;
            case ("DEF"):
                _Description.text = "Defends from enemy";
                break;
            case ("Change Equip"):
                _Description.text = "Change weapon";
                break;

            case ("Equipment 1"):
                Text currenttype = this.transform.Find("E1 Text").gameObject.GetComponentInChildren<Text>();
                if (currenttype.text == "Percussion")
                {
                   _Description.text = "Change weapon to Percussion";

               }else if(currenttype.text == "Wind")
                {
                    _Description.text = "Change weapon to Wind";

                }else if(currenttype.text == "String")
                {
                    _Description.text = "Change weapon to String";

                }else if(currenttype.text == "Unarmed")
                {
                    _Description.text = "Change back to your innate program";
                }
                //_Description.text = "Change weapon";
                break;

            case ("Equipment 2"):

                Text currenttype1 = this.transform.Find("E2 Text").gameObject.GetComponentInChildren<Text>();
                if (currenttype1.text == "Percussion")
                {
                    _Description.text = "Change weapon to Percussion";

                }
                else if (currenttype1.text == "Wind")
                {
                    _Description.text = "Change weapon to Wind";

                }
                else if (currenttype1.text == "String")
                {
                    _Description.text = "Change weapon to String";

                }
                else if (currenttype1.text == "Unarmed")
                {
                    _Description.text = "Change back to your innate program";
                }
                break;

            case ("Equipment 3"):

                Text currenttype2 = this.transform.Find("E3 Text").gameObject.GetComponentInChildren<Text>();
                if (currenttype2.text == "Percussion")
                {
                    _Description.text = "Change weapon to Percussion";

                }
                else if (currenttype2.text == "Wind")
                {
                    _Description.text = "Change weapon to Wind";

                }
                else if (currenttype2.text == "String")
                {
                    _Description.text = "Change weapon to String";

                }
                else if (currenttype2.text == "Unarmed")
                {
                    _Description.text = "Change back to your innate program";
                }
                break;

            case ("Whistle"):
                _Description.text = "Use your innate program to deal Little Damage \n" +
                    "MP usage: 0";
                break;

            case ("Sing"):
                _Description.text = "Show what you have learned for Medium Damage \n" +
                    "MP usage: 3";
                break;

            case ("PPlay"):
                _Description.text = "Strong Attack made by striking the drums \n" +
                    "MP usage: 5";
                break;
            case ("Fortississimo"):
                _Description.text = "Bang the drums to strike fear in your opponents \n" +
                    "MP usage: 3";
                break;
            case ("Echo"):
                _Description.text = "This attack reverberates through the room causing continuous damage to your opponent (1 turn) \n" +
                    "MP usage: 1";
                break;


            case ("SPlay"):
                _Description.text = "Strung the heart of the guitar for Medium Damage \n" +
                    "MP usage: 3";
                break;

            case ("STune"):
                _Description.text = "Attune your instruments to deal more damage (1 turn) \n" +
                    "MP usage: 3";
                break;

            case ("Criticize"):
                _Description.text = "Give the enemy a miss chance on their next attack \n" +
                    "MP usage: 1";

                break;

            case ("WPlay"):
                _Description.text = "Blow the trumpet for Very Little Damage \n" +
                    "MP usage: 0";
                break;
            case ("Repair"):
                _Description.text = "Fix damage that has been done \n" +
                    "MP usage: 2";
                break;
            case ("Charm"):
                _Description.text = "Claim the enemy's Music Points (MP) as your own \n" +
                    "MP usage: 0";
                break;



        }
      
        _Boxtext.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        _Boxtext.SetActive(false);

    }


}
