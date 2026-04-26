using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.SceneManagement;


public class VRPauseManager : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty primaryButtonLeft;
    public InputActionProperty primaryButtonRight;

    [Header("Menu Positioning")]
    public Transform xrCamera;
    public float menuDistance = 2f;
    public float menuHeightOffset = 0f;

    [Header("Interaction Scripts")]
    public Behaviour[] handInteractionScripts;

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    [Header("Locomotion Providers")]
    public Behaviour[] locomotionScripts;

    [Header("Points System")]
    public PointsSystem pointsSystem;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    private bool isPaused = false;

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void OnEnable()
    {
        primaryButtonLeft.action.Enable();
        primaryButtonRight.action.Enable();
    }

    void OnDisable()
    {
        primaryButtonLeft.action.Disable();
        primaryButtonRight.action.Disable();
    }

    void Update()
    {
        if (primaryButtonLeft.action.WasPressedThisFrame() ||
            primaryButtonRight.action.WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Vector3 forward = xrCamera.forward;
            forward.y = 0f;
            forward.Normalize();

            pauseMenu.transform.position =
                xrCamera.position + forward * menuDistance + Vector3.up * menuHeightOffset;

            pauseMenu.transform.rotation =
                Quaternion.LookRotation(-forward);
        }

        pauseMenu.SetActive(isPaused);

        if (isPaused && scoreText != null && pointsSystem != null)
        {
            scoreText.text = "Score: " + pointsSystem.scoredPoints.ToString("F1");
        }

        foreach (Behaviour script in locomotionScripts)
        {
            if (script != null)
                script.enabled = !isPaused;
        }

        foreach (Behaviour script in handInteractionScripts)
        {
            if (script != null)
                script.enabled = !isPaused;
        }

        if (pointsSystem != null)
        {
            pointsSystem.isRunning = !isPaused;
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }
}