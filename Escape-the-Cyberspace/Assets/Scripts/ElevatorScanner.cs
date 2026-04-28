using UnityEngine;
using TMPro; // Required for your elevator screen

public class ElevatorScanner : MonoBehaviour
{
    public RFIDListener listener;
    public Doors doors;

    [Header("Scanner Identity")]
    [Tooltip("Type the exact Job Role from the JSON that is allowed here.")]
    public string requiredRole = "Project Manager"; 

    [Header("UI Settings")]
    public TextMeshProUGUI statusDisplay; // Drag your Elevator's TMPro screen here

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip accessGrantedSound; 
    public AudioClip accessDeniedSound;

    private void Start()
    {
        // Automatically find AudioSource if not assigned
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        
        UpdateDisplay("ELEVATOR SECURED\nPLEASE SCAN", Color.white);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Search for the RFIDTag component in the object or its parent
        RFIDTag tag = other.GetComponentInParent<RFIDTag>();
        
        if (tag != null)
        {
            Debug.Log($"Elevator checking tag for role: {requiredRole}");

            // Call the loop logic in the listener to see if this ID belongs to a Project Manager
            if (listener.CheckAccessByRole(tag.storedID, requiredRole))
            {
                AccessGranted();
            }
            else
            {
                AccessDenied();
            }
        }
    }

    void AccessGranted()
    {
        UpdateDisplay("ACCESS GRANTED\nWELCOME", Color.green);
        
        if (audioSource && accessGrantedSound) 
            audioSource.PlayOneShot(accessGrantedSound);
            
        // Trigger the elevator door animation
        doors.Open();
    }

    void AccessDenied()
    {
        UpdateDisplay("UNAUTHORIZED\nACCESS DENIED", Color.red);
        
        if (audioSource && accessDeniedSound) 
            audioSource.PlayOneShot(accessDeniedSound);
    }

    void UpdateDisplay(string message, Color textColor)
    {
        if (statusDisplay != null)
        {
            statusDisplay.text = message;
            statusDisplay.color = textColor;
        }
    }
}