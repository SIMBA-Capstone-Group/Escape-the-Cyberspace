using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro; // <-- Added this to talk to the Sticky Notes

public class LoginManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboardUI;
    public InputField passwordInputField;
    public GameObject[] poweredOnScreens;
    public GameObject loginScreen;

    public AdminLoginManager admin;

    [Header("Password Setup")]
    [Tooltip("Drag the TextMeshPro object of the sticky note you want this computer to use.")]
    public TMP_Text linkedStickyNote; // <-- Added this slot to hold the sticky note

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

        // First, check if we linked a sticky note, and grab its text.
        // We use .Trim() to strip out any invisible spaces or newlines TextMeshPro might hide.
        string correctPassword = "";
        if (linkedStickyNote != null)
        {
correctPassword = linkedStickyNote.text.Replace("\u200B", "").Trim();        }

        // Now we compare what the player typed to the sticky note's text
        if (passwordInputField.text == correctPassword)
        {
            if (keyboardUI != null)
            {
                keyboardUI.SetActive(false);
            }
            if (poweredOnScreens != null)
            {
                foreach (GameObject screen in poweredOnScreens)
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
            if (admin != null)
            {
                admin.isLoggedIn = true;
            }
        }
        else if (onIncorrect != null)
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

    public void ChangeScreen(int index, GameObject screen)
    {
        poweredOnScreens[index] = screen;
    }
}