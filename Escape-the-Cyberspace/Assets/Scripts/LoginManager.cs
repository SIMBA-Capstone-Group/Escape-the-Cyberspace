using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Objects")]
    public GameObject visualKeyboard;
    public Text passwordInputField;  // Drag the 'Pass' text object from Monitor 2 here
    public GameObject atbashCanvas;   // The canvas for the right monitor

    [Header("Security")]
    public string correctPassword = "BadPassword123";

    // 1. This handles the INITIAL CLICK on the monitor screen
    public void OnPointerClick(PointerEventData eventData)
    {
        if (visualKeyboard != null)
        {
            visualKeyboard.SetActive(true);
            Debug.Log("Monitor Clicked: Showing Keyboard");
        }
    }

    // 2. This is called by the Keyboard's 'Enter' button
    public void ValidatePassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            UnlockAtbashPuzzle();
        }
        else
        {
            Debug.Log("Invalid Password");
            // Optional: visual feedback like turning text red
        }
    }

    private void UnlockAtbashPuzzle()
    {
        visualKeyboard.SetActive(false);
        if (atbashCanvas != null)
        {
            atbashCanvas.SetActive(true);
        }
    }
}