using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    public string levelNumber;
    public double startingPoints;
    public double scoredPoints;
    public bool isRunning = true;
    public int ticksPerUpdate = 0;
    private int ticksToFrameUpdate;
    private string savePath;
    public Transform leaderboardLocation;
    public GameObject leaderboardPrefab;

    private ScoreData scores;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        savePath = Application.persistentDataPath + "/level" + levelNumber + "score.json";
        Debug.Log(savePath);
        scores = LoadScore(); // try loading first

        
        if(startingPoints != 0)
        {
            isRunning = true;
            scoredPoints += startingPoints;
        }
        else
        {
            Debug.Log("No starting value entered!");
        }

        if(leaderboardLocation != null)
        {
            GenerateLeaderboard();
        }

        ticksToFrameUpdate += ticksPerUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        if((scoredPoints > 0) && isRunning && (ticksToFrameUpdate == 0))
        {
            scoredPoints -= startingPoints * 0.001;
            ticksToFrameUpdate += ticksPerUpdate;
        }
        else if (isRunning)
        {
            ticksToFrameUpdate -= 1;
        }
    }

    public void IncorrectAnswer()
    {
        if((scoredPoints > 0) && isRunning)
        {
            scoredPoints -= startingPoints * .01;
        }
    }

    public void stopScoring()
    {
        isRunning = false;
        SaveScore("default");
    }

    public void SaveScore(string playerName)
    {
        ScoreEntry newEntry = new ScoreEntry
        {
            playerName = playerName,
            score = scoredPoints
        };

        scores.entries.Add(newEntry);

        string json = JsonUtility.ToJson(scores, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved score for: " + playerName);
    }

    public ScoreData LoadScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            data.entries ??= new List<ScoreEntry>();

            return data;
        }

        return new ScoreData();
    }

    public List<ScoreEntry> GetTopScores(int count = 3)
    {
        // Sort descending by score
        scores.entries.Sort((a, b) => b.score.CompareTo(a.score));

        // Clamp in case there are fewer than 3
        int take = Mathf.Min(count, scores.entries.Count);

        return scores.entries.GetRange(0, take);
    }

    public void GenerateLeaderboard()
    {
        List<ScoreEntry> topScores = GetTopScores();

        if (topScores == null || topScores.Count == 0)
        {
            Debug.Log("Leaderboard is empty!");
        }

        int maxSlots = 3;

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject leaderboardInstance = Instantiate(leaderboardPrefab, leaderboardLocation);
            if (i < topScores.Count)
            {
                // Set Text
                TextMeshProUGUI textComponent = leaderboardInstance.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    leaderboardInstance.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = topScores[i].playerName;
                    leaderboardInstance.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = topScores[i].score.ToString("F1");
                }           
                Debug.Log($"{i + 1}. {topScores[i].playerName} - {topScores[i].score}");
            }
            else
            {
                TextMeshProUGUI textComponent = leaderboardInstance.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    leaderboardInstance.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "---";
                    leaderboardInstance.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "---";
                } 
                Debug.Log($"{i + 1}. ---");
            }
        }
    }
}

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public double score;
}

[System.Serializable]
public class ScoreData
{
    public List<ScoreEntry> entries = new();
}