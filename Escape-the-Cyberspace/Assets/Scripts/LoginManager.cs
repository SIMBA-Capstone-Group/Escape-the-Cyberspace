using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Objects")]
    public GameObject visualKeyboard;
    public Text passwordInputField;  // Drag the 'Pass' text object from Monitor 2 here
    public GameObject atbashCanvas;   // Drag the hidden Atbash Canvas here

    [Header("Security")]
    public string correctPassword = "BadPassword123";

    // 1. This triggers when the VR Raycast clicks the Monitor Screen
    public void OnPointerClick(PointerEventData eventData)
    {
        if (visualKeyboard != null)
        {
            visualKeyboard.SetActive(true);
            Debug.Log("Monitor Clicked: Showing Keyboard");
        }
    }

    // 2. This is called by the 'Enter' button on the Visual Keyboard
    public void ValidatePassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            UnlockAtbashPuzzle();
        }
        else
        {
            Debug.Log("Invalid Password: " + passwordInputField.text);
        }
    }

    private void UnlockAtbashPuzzle()
    {
        // Hide keyboard and show the Atbash PNG
        if (visualKeyboard != null) visualKeyboard.SetActive(false);

        if (atbashCanvas != null)
        {
            atbashCanvas.SetActive(true);
        }
    }
}