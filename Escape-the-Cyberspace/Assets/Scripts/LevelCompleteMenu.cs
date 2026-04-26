using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    public PointsSystem pointsSystem;

    private void OnEnable()
    {
        if (pointsSystem != null && scoreText != null)
        {
            scoreText.text = "Score: " + pointsSystem.scoredPoints.ToString("F1");
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Office");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}