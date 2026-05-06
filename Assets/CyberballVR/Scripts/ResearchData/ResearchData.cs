using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;
using System;

public class ResearchData : MonoBehaviour
{
    public static List<PlayerData> AIPlayers;
    public static List<string> throwList = new List<string>();
    public static List<string> catchList = new List<string>();

    [SerializeField]
    public static LevelData LevelData { get; private set; }

    public static List<string> namePool = new List<string> {"JORDAN", "MORGAN", "TAYLOR", "SAM", "CARTER", "ALEX", "HARPER", "RILEY", "AVERY", "PARKER"};

    
    private void Awake()
    {
        AIPlayers = LoadAllPlayers();
        LevelData = LoadLevelData();
        throwList.Add("The player threw the ball to ");
    }


    public List<PlayerData> LoadAllPlayers()
    {
        List<PlayerData> players = new List<PlayerData>();
        // string directoryPath = Application.persistentDataPath;

        // foreach (string file in Directory.GetFiles(directoryPath, "*.xml"))
        // {
        //     if (Path.GetFileName(file) != "Level.xml")
        //     {
        //         players.Add(LoadPlayerData(file, Path.GetFileName(file)));
        //     }
        //     Debug.Log("player: " + Path.GetFileName(file) + ", ");
        // }

        players.Add(LoadPlayerData());
        players.Add(LoadPlayerData());
        players.Add(LoadPlayerData());
        
        return players;
    }

    private PlayerData LoadPlayerData()
    {
        // try
        // {
        //     XDocument xmlDoc = XDocument.Load(filePath);
        //     PlayerData player = new PlayerData
        //     {
        //         Name = fileName.Split(".")[0],
        //         ThrowCount = 0,
        //         SkinColor = "Gray",
        //         // Hair = xmlDoc.Root.Element("Hair")?.Value,
        //         // Clothing = xmlDoc.Root.Element("Clothing")?.Value,
        //         // Accessory_1 = xmlDoc.Root.Element("Accessories1")?.Value,
        //         // Accessory_2 = xmlDoc.Root.Element("Accessories2")?.Value,
        //         //Clothing_Accessory_1 = xmlDoc.Root.Element("ClothingAccessory1")?.Value,
        //         //Clothing_Accessory_2 = xmlDoc.Root.Element("HeadAccessory2")?.Value,
        //     };

        //     return player;
        // }
        // catch (System.Exception e)
        // {
        //     Debug.LogError("Error loading player data from file " + filePath + ": " + e.Message);
        //     return null;
        // }

        PlayerData player = new PlayerData
        {
            Name = GenerateName(),
            ThrowCount = 0,
            SkinColor = "Gray",
        };

        return player;
    }

    private LevelData LoadLevelData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Level.xml");
        Debug.Log("Loading Level Data from file: " + filePath);

        LevelData levelData = new LevelData();
        XDocument xmlDoc;
        try
        {
            xmlDoc = XDocument.Load(filePath);

            // Parse RoundOneLength
            XElement roundOneLengthElement = xmlDoc.Root.Element("RoundOneLength");
            if (roundOneLengthElement != null)
            {
                levelData.RoundOneLength = int.Parse(roundOneLengthElement.Value);
                Debug.Log("Parsed RoundOneLength: " + levelData.RoundOneLength);
            }
            else
            {
                Debug.LogWarning("RoundOneLength element is missing in the XML.");
            }

            // Parse RoundTwoLength
            XElement roundTwoLengthElement = xmlDoc.Root.Element("RoundTwoLength");
            if (roundTwoLengthElement != null)
            {
                levelData.RoundTwoLength = int.Parse(roundTwoLengthElement.Value);
                Debug.Log("Parsed RoundTwoLength: " + levelData.RoundTwoLength);
            }
            else
            {
                Debug.LogWarning("RoundTwoLength element is missing in the XML.");
            }

            // Parse RoundThreeLength
            XElement roundThreeLengthElement = xmlDoc.Root.Element("RoundThreeLength");
            if (roundThreeLengthElement != null)
            {
                levelData.RoundThreeLength = int.Parse(roundThreeLengthElement.Value);
                Debug.Log("Parsed RoundThreeLength: " + levelData.RoundThreeLength);
            }
            else
            {
                Debug.LogWarning("RoundThreeLength element is missing in the XML.");
            }

            // Parse NoOfThrows
            XElement noOfThrowsElement = xmlDoc.Root.Element("NoOfThrows");
            if (noOfThrowsElement != null)
            {
                levelData.NoOfThrows = int.Parse(noOfThrowsElement.Value);
                Debug.Log("Parsed NoOfThrows: " + levelData.NoOfThrows);
            }
            else
            {
                Debug.LogWarning("NoOfThrows element is missing in the XML.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Failed to load XML Document: " + e.Message);

            levelData.RoundOneLength = 48;
            levelData.RoundTwoLength = 20;
            levelData.RoundThreeLength = 48;
            levelData.NoOfThrows = levelData.RoundOneLength + levelData.RoundTwoLength + levelData.RoundThreeLength;
        }

        // // Parse ChancesToPlayer
        // int cumulativeThrows = 0;
        // levelData.ChancesToPlayer.Clear();
        // XElement root = xmlDoc.Root.Element("ChancesToPlayer");
        // foreach (var element in root.Value.Split(';'))
        // {
        //     if (!string.IsNullOrWhiteSpace(element))
        //     {
        //         var parts = element.Split(',');
        //         int throwCount = int.Parse(parts[0]); // Throws 
        //         float chanceValue = float.Parse(parts[1]); // Chance 

        //         cumulativeThrows += throwCount;
        //         levelData.ChancesToPlayer.Add(new ChanceToPlayer { Throws = cumulativeThrows, Chance = chanceValue });

        //         Debug.Log($"Loaded chance {chanceValue}% for up to {cumulativeThrows} throws.");
        //     }
        // }

        // // Parse Speeds
        // cumulativeThrows = 0;
        // levelData.Speeds.Clear();
        // root = xmlDoc.Root.Element("Speeds");

        // foreach (var element in root.Value.Split(';'))
        // {
        //     if (!string.IsNullOrWhiteSpace(element))
        //     {
        //         var parts = element.Split(',');
        //         int throwCount = int.Parse(parts[0]); // Throws
        //         float speedValue = float.Parse(parts[1]); // Speed

        //         cumulativeThrows += throwCount;
        //         levelData.Speeds.Add(new Speed { Throws = cumulativeThrows, SpeedValue = speedValue });

        //         Debug.Log($"Loaded speed {speedValue} for up to {cumulativeThrows} throws.");
        //     }
        // }

        // foreach (var speed in levelData.Speeds)
        // {
        //     Debug.Log($"Speed setting: {speed.Throws} throws at {speed.SpeedValue} speed");
        // }
        // Debug.Log("Speed size " + levelData.Speeds.Count);
        return levelData;
    }


    public static void saveLog()
    {
        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss.fff");
        StreamWriter sw = File.CreateText(Application.persistentDataPath + "\\Log - " + timestamp + ".txt");
        for(int i = 0; i < catchList.Count; i++)
        {
            if(i == 0)
            {
                sw.WriteLine("=== Round 1 (Tosses 1-" + LevelData.RoundOneLength + ") ===");
            }
            if(i == LevelData.RoundOneLength)
            {
                sw.WriteLine("");
                foreach(PlayerData aiPlayer in AIPlayers)
                {
                    sw.WriteLine("Player tossed to " + aiPlayer.Name + " " + aiPlayer.ThrowCount + " times");
                    aiPlayer.ThrowCount = 0;
                }
                sw.WriteLine("\n\n=== Round 2 (Tosses " + (LevelData.RoundOneLength + 1) + "-" + (LevelData.RoundOneLength + LevelData.RoundTwoLength) + ") ===");
            }
            if(i == (LevelData.RoundOneLength + LevelData.RoundTwoLength))
            {
                sw.WriteLine("");
                foreach(PlayerData aiPlayer in AIPlayers)
                {
                    sw.WriteLine("Player tossed to " + aiPlayer.Name + " " + aiPlayer.ThrowCount + " times");
                    aiPlayer.ThrowCount = 0;
                }
                sw.WriteLine("\n\n=== Round 3 (Tosses " + (LevelData.RoundOneLength + LevelData.RoundTwoLength + 1) + "-" + (LevelData.NoOfThrows) + ") ===");
            }

            // switch(i) {
            //     case 0:
            //         sw.WriteLine("=== Round 1 (Tosses 1-" + LevelData.RoundOneLength + ") ===");
            //         break;
            //     case 48:
            //         sw.WriteLine("\n");
            //         foreach(PlayerData aiPlayer in AIPlayers)
            //         {
            //             sw.WriteLine("Player tossed to " + aiPlayer.Name + " " + aiPlayer.ThrowCount + " times");
            //             aiPlayer.ThrowCount = 0;
            //         }
            //         sw.WriteLine("\n=== Round 2 (Tosses " + (LevelData.RoundOneLength + 1) + "-" + (LevelData.RoundOneLength + LevelData.RoundTwoLength) + ") ===");
            //         break;
            //     case 68:
            //         sw.WriteLine("\n");
            //         foreach(PlayerData aiPlayer in AIPlayers)
            //         {
            //             sw.WriteLine("Player tossed to " + aiPlayer.Name + " " + aiPlayer.ThrowCount + " times");
            //             aiPlayer.ThrowCount = 0;
            //         }
            //         sw.WriteLine("\n=== Round 3 (Tosses " + (LevelData.RoundOneLength + LevelData.RoundTwoLength + 1) + "-" + (LevelData.NoOfThrows) + ") ===");
            //         break;
            // }

            if(throwList[i] == "The player threw the ball to ")
            {
                foreach(PlayerData aiPlayer in AIPlayers)
                {
                    if(aiPlayer.Name == catchList[i])
                    {
                        aiPlayer.ThrowCount += 1;
                    }
                }
            }

            sw.WriteLine((i + 1) + ": " + throwList[i] + catchList[i]);

            if(i == (LevelData.NoOfThrows - 1))
            {
                sw.WriteLine("");
                foreach(PlayerData aiPlayer in AIPlayers)
                {
                    sw.WriteLine("Player tossed to " + aiPlayer.Name + " " + aiPlayer.ThrowCount + " times");
                    aiPlayer.ThrowCount = 0;
                }
            }
        }

        sw.Close();
    }

    private String GenerateName()
    {
        int randomNumber = UnityEngine.Random.Range(0, namePool.Count);
        string randomName = namePool[randomNumber];
        namePool.Remove(randomName);
        return randomName;
    }
}

public class PlayerData
{
    public string Name;
    public int ThrowCount;
    public string Hair;
    public string SkinColor;
    public string Clothing;
    public string Accessory_1;
    public string Accessory_2;
    //public string Clothing_Accessory_1;
    //public string Clothing_Accessory_2;

}

public class LevelData
{
    public int RoundOneLength { get; set; }
    public int RoundTwoLength { get; set; }
    public int RoundThreeLength { get; set; }
    public int NoOfThrows { get; set; }
    public List<ChanceToPlayer> ChancesToPlayer { get; set; }
    public List<Speed> Speeds { get; set; }

    public LevelData()
    {
        ChancesToPlayer = new List<ChanceToPlayer>();
        Speeds = new List<Speed>();
    }
}

public class ChanceToPlayer
{
    public int Throws { get; set; }
    public float Chance { get; set; }
}

public class Speed
{
    public int Throws { get; set; }
    public float SpeedValue { get; set; }
}
