using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // <-- Added this to read the Sticky Note!

// same as LoginManager script, but used for the printer computer in lvl 2

public class LoginManagerPrinter : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboardUI;
    public InputField passwordInputField;
    public GameObject[] poweredOnScreens;
    public GameObject loginScreen;
    
    [Header("Password Setup")]
    [Tooltip("Drag the TextMeshPro object of the sticky note you want this computer to use.")]
    public TMP_Text linkedStickyNote; // <-- Replaced the hardcoded string with this!

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
        
        // Grab the text straight from the sticky note and remove any hidden spaces
        string correctPassword = "";
        if (linkedStickyNote != null)
        {
            correctPassword = linkedStickyNote.text.Trim();
        }

        // Check the player's input against the sticky note
        if (passwordInputField.text == correctPassword)
        {
            // print paper at printer
            
            paperObject.transform.position = printerSpawnPoint.position;
            Debug.Log(paperObject.transform.position);
            paperObject.transform.rotation = printerSpawnPoint.rotation;
            Debug.Log(paperObject.transform.rotation);

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