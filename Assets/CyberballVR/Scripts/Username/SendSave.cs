using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendSave : MonoBehaviour
{
    public TMP_Text username;

    public void SaveTyped()
    {
        username.SendMessage("SaveUsername");
    }
}
