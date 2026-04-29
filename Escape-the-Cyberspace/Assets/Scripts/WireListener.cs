using UnityEngine;
using UnityEngine.Events;

public class WireListener : MonoBehaviour
{
    [Header("Wiring Settings")]
    [Tooltip("Order must match hierarchy! Socket index must match plug index.")]
    public WireFemaleSocket[] sockets;
    public WireMalePlug[] plugs;

    [Header("Feedback Settings")]
    public AudioSource successAudioSource; // Drag your AudioSource here
    public UnityEvent onCorrect;

    private bool hasTriggered = false; // Prevents the sound/event from firing multiple times

    void Start()
    {
        if (sockets.Length != plugs.Length)
        {
            Debug.LogWarning("Not enough plugs/sockets on " + gameObject.name);
        }

        SocketSetup();
        PlugSetup();
    }

    void SocketSetup()
    {
        foreach (WireFemaleSocket socket in sockets)
        {
            socket.acceptedWireID = "id";
        }
    }

    void PlugSetup()
    {
        foreach (WireMalePlug plug in plugs)
        {
            plug.wireID = "id";
        }
    }

    public void CheckCorrectness()
    {
        // Don't check if we already finished the puzzle
        if (hasTriggered) return;

        foreach (WireFemaleSocket socket in sockets)
        {
            if (!socket.isCorrect)
            {
                Debug.Log("One or more wires are still incorrect.");
                return;
            }
        }

        // --- SUCCESS LOGIC ---
        Debug.Log("All wires correct!");
        hasTriggered = true; // Mark as done

        // Play the audio if assigned
        if (successAudioSource != null)
        {
            successAudioSource.Play();
        }

        // Trigger any other events (like opening the door or enabling the collider)
        onCorrect.Invoke();
    }
}