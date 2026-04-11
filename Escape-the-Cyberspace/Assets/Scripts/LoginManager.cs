using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboardUI;
    public InputField passwordInputField;
    public GameObject [] poweredOnScreens;
    public GameObject loginScreen;
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
        Debug.Log(passwordInputField.text);
        if (passwordInputField.text == correctPassword)
        {
            if (keyboardUI != null)
            {
                keyboardUI.SetActive(false);
            }
            if (poweredOnScreens != null)
            {
                foreach(GameObject screen in poweredOnScreens)
                {
                    screen.SetActive(true);
                }
            }
            if (loginScreen != null)
            {
                loginScreen.SetActive(false);
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