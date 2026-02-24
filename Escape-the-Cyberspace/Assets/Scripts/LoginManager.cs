using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboardUI;
    public InputField passwordInputField;
    public GameObject atbashCanvas;
    public string correctPassword = "BadPassword123";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (keyboardUI != null)
        {
            keyboardUI.SetActive(true);
        }
    }

    public void ValidatePassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            if (keyboardUI != null)
            {
                keyboardUI.SetActive(false);
            }
            if (atbashCanvas != null)
            {
                atbashCanvas.SetActive(true);
            }
        }
    }

    public void ClearPassword()
    {
        if (passwordInputField != null)
        {
            passwordInputField.text = "";
        }
    }
}