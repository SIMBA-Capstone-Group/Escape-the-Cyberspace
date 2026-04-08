using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PointsSystem : MonoBehaviour
{
    public double startingPoints;
    public double scoredPoints;
    public bool isRunning = true;
    public int ticksPerUpdate = 0;
    private int ticksToFrameUpdate;
    private string savePath;
    private List<double> scoreHistory = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        savePath = Application.persistentDataPath + "/score.json";
        LoadScore(); // try loading first

        isRunning = true;
        if(startingPoints != 0)
        {
            scoredPoints += startingPoints;
        }
        else
        {
            Debug.Log("No starting value entered!");
        }

        ticksToFrameUpdate += ticksPerUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        if((scoredPoints > 0) && isRunning && (ticksToFrameUpdate == 0))
        {
            scoredPoints -= startingPoints * 0.01;
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
            scoredPoints -= startingPoints * .02;
        }
    }

    public void stopScoring()
    {
        isRunning = false;
    }

     public void SaveScore()
    {
        scoreHistory.Add(scoredPoints);

        ScoreData data = new ScoreData
        {
            currentScore = scoredPoints,
            scoreHistory = scoreHistory.ToArray()
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved to: " + savePath);
    }

    public void LoadScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            scoredPoints = data.currentScore;
            scoreHistory = data.scoreHistory != null 
            ? new List<double>(data.scoreHistory) 
            : new List<double>();

            Debug.Log("Loaded score: " + scoredPoints);
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public double currentScore;
    public double[] scoreHistory;
}