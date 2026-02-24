using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsernameText : MonoBehaviour
{
    string username = "________";
    // public GameObject customizationManager;

    // Start is called before the first frame update
    void Awake()
    {
        username = UsernameVariable.playerUsername;
        GetComponent<TMP_Text>().text = username;
    }

    void AddLetter(char letter)
    {
        char[] characters = username.ToCharArray();
        for(int i = 0; i < characters.Length; i++)
        {
            if(characters[i] == '_')
            {
                characters[i] = letter;
                break;
            }
        }
        username = new string(characters);
        GetComponent<TMP_Text>().text = username;
    }

    void AddSpace()
    {
        char[] characters = username.ToCharArray();
        for(int i = 0; i < characters.Length; i++)
        {
            if(characters[i] == '_')
            {
                characters[i] = ' ';
                break;
            }
        }
        username = new string(characters);
        GetComponent<TMP_Text>().text = username;
    }

    void RemoveLetter()
    {
        char[] characters = username.ToCharArray();
        for(int i = characters.Length - 1; i >= 0; i--)
        {
            if(characters[i] != '_')
            {
                characters[i] = '_';
                break;
            }
        }
        username = new string(characters);
        GetComponent<TMP_Text>().text = username;
    }

    void SaveUsername()
    {
        UsernameVariable.playerUsername = username;
    }
}
