using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketTeleporter : MonoBehaviour
{
    public GameObject noteObject;    // The note waiting outside the map
    public Transform targetLocation; // A Transform (Empty GameObject) where the note should go
    
    private bool hasTeleported = false;

    public void TeleportNote(SelectEnterEventArgs args)
    {
        if (!hasTeleported)
        {
            // Move the note to the target's position and rotation
            noteObject.transform.position = targetLocation.position;
            noteObject.transform.rotation = targetLocation.rotation;

            // Optional: Play a sound or particle effect at the targetLocation
            hasTeleported = true;
        }
    }
}