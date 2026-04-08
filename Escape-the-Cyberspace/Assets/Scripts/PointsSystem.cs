using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    public double startingPoints;
    public double scoredPoints;
    public bool isRunning = true;
    public int ticksPerUpdate = 0;
    private int ticksToFrameUpdate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
}
