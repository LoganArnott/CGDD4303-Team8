using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendLetter : MonoBehaviour
{
    public TMP_Text username;
    char letter;

    // Start is called before the first frame update
    void Start()
    {
        letter = GetComponentInChildren<TMP_Text>().text[0];
    }

    public void LetterTyped()
    {
        username.SendMessage("AddLetter", letter);
    }
}
