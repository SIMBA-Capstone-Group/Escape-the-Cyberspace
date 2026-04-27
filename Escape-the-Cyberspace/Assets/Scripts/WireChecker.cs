using UnityEngine;

public class WirePuzzleManager : MonoBehaviour
{
    public WireSocket[] sockets;

    private void OnEnable()
    {
        WireSocket.ConnectionChanged += CheckSolution;
    }

    private void OnDisable()
    {
        WireSocket.ConnectionChanged -= CheckSolution;
    }

    void CheckSolution()
    {
        foreach (WireSocket socket in sockets)
        {
            if (!socket.IsCorrect())
            {
                Debug.Log("Incorrect configuration");
                return;
            }
        }

        Debug.Log("Puzzle Solved!");
        OnPuzzleSolved();
    }

    void OnPuzzleSolved()
    {
        // Do your win logic here
        // open door, play sound, etc.
    }
}