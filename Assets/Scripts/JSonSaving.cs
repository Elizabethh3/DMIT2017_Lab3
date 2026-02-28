using UnityEngine;
using System.IO;

public class JSonSaving : MonoBehaviour
{
    public string filePath;
    public SaveData profileData;
    string profileName;
    [ContextMenu("JSON Save")]

    public void SaveData()
    {
        string json = JsonUtility.ToJson(GameStateManager.Instance.gameState, true);
        File.WriteAllText(filePath, json);   
    }

/*    public void SaveData(SaveData profile_)
    {
        string file = filePath + profile_.profileName + ".json";
        string json = JsonUtility.ToJson(profile_, true);
        File.WriteAllText(file, json);
    }*/

    [ContextMenu("JSON Load")]

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameStateManager.Instance.gameState = JsonUtility.FromJson<GameState>(json);
            GameStateManager.Instance.InitializeDictionaries();
        }

        else
        {
            Debug.LogError("Save file not found");
        }
    }

    public void ResetData()
    {
        FindAnyObjectByType<Player>().ResetPlayerGold();
        FindAnyObjectByType<Player>().ResetPlayerHeath();
        GameStateManager.Instance.ResetEnemies();
        Treasure[] treasures = FindObjectsByType<Treasure>(FindObjectsSortMode.None);
        foreach (Treasure t in treasures)
        {
            t.ResetTreasure();
        }
    }
}
