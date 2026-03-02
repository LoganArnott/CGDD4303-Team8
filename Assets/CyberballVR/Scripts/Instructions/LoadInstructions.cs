using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class LoadInstructions : MonoBehaviour
{
    public TMP_Text instructionsOne;
    public TMP_Text instructionsTwo;
    public TMP_Text instructionsThree;

    private void Awake()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Level.xml");

        XDocument xmlDoc;
        try
        {
            xmlDoc = XDocument.Load(filePath);

            // Parse Instructions
            XElement root = xmlDoc.Root.Element("InstructionsOne");
            instructionsOne.text = root.Value.ToString();
            
            root = xmlDoc.Root.Element("InstructionsTwo");
            instructionsTwo.text = root.Value.ToString();
            
            root = xmlDoc.Root.Element("InstructionsThree");
            instructionsThree.text = root.Value.ToString();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load XML Document: " + e.Message);
        }
    }
}
