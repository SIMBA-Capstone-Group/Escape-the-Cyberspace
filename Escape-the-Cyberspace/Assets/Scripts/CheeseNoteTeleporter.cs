using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketTeleporter : MonoBehaviour
{
    // note and where note is supposed to teleport to
    public GameObject noteObject;    
    public Transform targetLocation; 
 
    // has the note already been teleported?
    private bool hasTeleported = false;

    public void TeleportNote(SelectEnterEventArgs args)
    {
	// if the note hasn't been teleported already
        if (!hasTeleported)
        {
            // Move the note to the location it's supposed to appear
            noteObject.transform.position = targetLocation.position;
            noteObject.transform.rotation = targetLocation.rotation;
	
	    hasTeleported = true;    
        }
    }
}