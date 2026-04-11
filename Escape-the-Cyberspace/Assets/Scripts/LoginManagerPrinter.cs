using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// same as LoginManager script, but used for the printer computer in lvl 2

public class LoginManagerPrinter : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboardUI;
    public InputField passwordInputField;
    public GameObject[] poweredOnScreens;
    public GameObject loginScreen;
    public string correctPassword = "BadPassword123";
    public KeyboardPlacementCheck KeyboardListener;

    [Header("Printer Settings")]
    public GameObject paperObject;      // paper object
    public Transform printerSpawnPoint; // empty object for paper to spawn at
    public AudioSource printerAudio;    // printer sound source

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
            // print paper at printer
            
            paperObject.transform.position = printerSpawnPoint.position;
            paperObject.transform.rotation = printerSpawnPoint.rotation

            // Play the print sound
            if (printerAudio != null)
            {
                printerAudio.Play();
            }

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