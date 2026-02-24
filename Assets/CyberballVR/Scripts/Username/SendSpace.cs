using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendSpace : MonoBehaviour
{
    public TMP_Text username;

    public void SpaceTyped()
    {
        username.SendMessage("AddSpace");
    }
}
