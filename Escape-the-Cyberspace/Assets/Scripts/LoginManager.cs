using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboardUI;
    public InputField passwordInputField;
    public GameObject atbashCanvas;
    public string correctPassword = "BadPassword123";
    public KeyboardPlacementCheck KeyboardListener;
    public UnityEvent onIncorrect;

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
            if (KeyboardListener != null)
            {
                KeyboardListener.NoMoreUIPlease();
            }
        }
        else if(onIncorrect != null)
        {
            onIncorrect.Invoke();
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