using UnityEngine;

public class KeySnapTrigger : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public PointsSystem pointsSystem;

    private bool completed = false;

    public void OnKeySnapped()
    {
        if (completed) return;
        completed = true;

        pointsSystem.stopScoring();

        levelCompleteUI.SetActive(true);

        // Optional:
        Time.timeScale = 0f;
    }
}