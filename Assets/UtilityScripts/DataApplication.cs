using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using EnumLibrary;

public class DataApplication : MonoBehaviour
{
    public enum TypeOfData
    {
        status,
        move_names,
        hp,
        name,
        type,
        speed
    }

    public TypeOfData type;

    public Text output;
    public InputField input;

    public void LoadChanges(Pokemon index)
    {
        switch (type)
        {
            case TypeOfData.hp:
                output.text = "HP: " + index.pMaxHp;
                if (index.pMaxHp != 0)
                    input.text = index.pMaxHp.ToString();
                else
                    input.text = "";
                break;

            case TypeOfData.name:
                output.text = "Name: " + index.pName;
                if (index.pName != "")
                    input.text = index.pName.ToString();
                else
                    input.text = "";
                break;

            case TypeOfData.move_names:
                output.text = "Is scary: " + index.pMoves_Names;
                input.text = index.monsterScariness.ToString();
                break;

            case TypeOfData.type:
                output.text = "Type: " + index.pTypes;
                if (index.pTypes != Pokemon.MonsterTypes.none)
                    input.text = index.pTypes.ToString();
                else
                    input.text = "";
                break;

            case TypeOfData.xp:
                output.text = "XP: " + index.monsterXP;
                if (index.monsterXP != 0)
                    input.text = index.monsterXP.ToString();
                else
                    input.text = "";
                break;
        }
    }

    public void SaveChanges(Monster index)
    {
        switch (type)
        {
            case TypeOfData.hp:
                int.TryParse(input.text, out index.monsterHP);
                break;

            case TypeOfData.name:
                index.monsterName = input.text;
                break;

            case TypeOfData.scary:
                bool.TryParse(input.text, out index.monsterScariness);
                break;

            case TypeOfData.xp:
                float.TryParse(input.text, out index.monsterXP);
                break;

            case TypeOfData.type:
                if (input.text == "")
                    index.monsterType = Monster.MonsterTypes.none;
                else
                    try
                    {
                        index.monsterType = (Monster.MonsterTypes)Enum.Parse(typeof(Monster.MonsterTypes), input.text, true);
                    }
                    catch (ArgumentException e)
                    {
                        Debug.Log(input.text + " Isn't in the enum list");
                        e.ToString();
                        index.monsterType = Monster.MonsterTypes.none;
                    }
                break;
        }
    }
}