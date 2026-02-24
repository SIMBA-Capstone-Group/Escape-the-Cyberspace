using UnityEngine;
using UnityEngine.EventSystems; // Required for clicking

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject visualKeyboard; // Drag your keyboard object here

    // This function runs automatically when the VR raycast clicks this object
    public void OnPointerClick(PointerEventData eventData)
    {
        if (visualKeyboard != null)
        {
            visualKeyboard.SetActive(true);
            Debug.Log("Monitor clicked - Opening Keyboard");
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