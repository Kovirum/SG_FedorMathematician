using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{


    public static List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
    public Text[] LeaderboardUI;

    public static void SaveLeaderboard()
    {
        LeaderboardData data = new LeaderboardData();
        data.entries = leaderboard;
        string jsonData = JsonUtility.ToJson(data);

        string filePath = Path.Combine(Application.dataPath, "Resources/leaderboard.json");

        File.WriteAllText(filePath, jsonData);
    }

    public static void LoadLeaderboard()
    {
        //TextAsset jsonFile = Resources.Load<TextAsset>("leaderboard.json");

        string jsonData = File.ReadAllText(Path.Combine(Application.dataPath, "Resources/leaderboard.json"));
        LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(jsonData);
        leaderboard = data.entries;
    }

    public static void Add(string name, int score)
    {
        LeaderboardEntry existingEntry = leaderboard.Find(entry => entry.Name == name);

        if (existingEntry != null)
        {
            existingEntry.Score = existingEntry.Score > score ? existingEntry.Score : score;
        }
        else
        {
            LeaderboardEntry leaderboardEntry = new LeaderboardEntry();
            leaderboardEntry.Name = name;
            leaderboardEntry.Score = score;
            leaderboard.Add(leaderboardEntry);
        }
        SaveLeaderboard();
    }


    void Start()
    {
        LoadLeaderboard();
        UpdateLeaderboard();
    }

    void UpdateLeaderboard()
    {
        List<LeaderboardEntry> leaderboard = LeaderboardManager.leaderboard;
        leaderboard.Sort((entry1, entry2) => entry2.Score.CompareTo(entry1.Score));

        for (int i = 0; i < LeaderboardUI.Length; i++)
        {
            if (i < leaderboard.Count)
            {
                LeaderboardUI[i].text = $"{leaderboard[i].Name} - {leaderboard[i].Score}";
            }
            else
            {
                break;
            }
        }

    }



}

[System.Serializable]
public class LeaderboardEntry
{
    public string Name;
    public int Score;
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries;
}

