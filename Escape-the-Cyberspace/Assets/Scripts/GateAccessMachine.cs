using Unity.VisualScripting;
using UnityEngine;

public class GateAccessMachine : MonoBehaviour
{
    public RFIDListener listener;
    public Doors doors;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip accessGrantedSound; 
    public AudioClip accessDeniedSound;

    private void Start()
    {
        // Automatically try to find the AudioSource if you forgot to drag it in
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RFIDTag tag = other.GetComponentInParent<RFIDTag>();
        if (tag != null)
        {
            Debug.Log("Tag entered scanner!");

            // check against correct ID
            if (listener.GetCorrectRFID() == tag.storedID)
            {
                Debug.Log("Access granted!");

                // correct audio
                if (audioSource != null && accessGrantedSound != null)
                {
                    audioSource.PlayOneShot(accessGrantedSound);
                }

                doors.Open();
            }
            else
            {
                Debug.Log("Access denied!");

                // Fail audio
                if (audioSource != null && accessDeniedSound != null)
                {
                    audioSource.PlayOneShot(accessDeniedSound);
                }
            }
        }
    }
}