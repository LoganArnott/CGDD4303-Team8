using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeleteLetter : MonoBehaviour
{
    public TMP_Text username;

    public void DeleteTyped()
    {
        username.SendMessage("RemoveLetter");
    }
}
