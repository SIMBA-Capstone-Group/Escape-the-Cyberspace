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

        if (pointsSystem != null)
            pointsSystem.stopScoring();

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(true);

        Time.timeScale = 0f;
    }
}