using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRPauseManager : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty primaryButtonLeft;   // X button
    public InputActionProperty primaryButtonRight;  // A button

    [Header("Menu Positioning")]
    public Transform xrCamera;
    public float menuDistance = 2f;
    public float menuHeightOffset = 0f;

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    [Header("Locomotion Providers")]
    public Behaviour[] locomotionScripts; 
    // Drag your move/turn providers here in inspector

    private bool isPaused = false;

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
            Quaternion.LookRotation(forward);
    }

    pauseMenu.SetActive(isPaused);

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

    Time.timeScale = isPaused ? 0f : 1f;
}
}