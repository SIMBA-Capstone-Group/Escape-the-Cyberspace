using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class WireFemaleSocket : MonoBehaviour
{
    public string acceptedWireID;
    public Transform snapPoint;
    public UnityEvent onCorrectWirePlugged;
    public bool isCorrect = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    public void OnPluggedIn(SelectEnterEventArgs args)
    {
        Debug.Log("Plugged in!");

        var go = args.interactableObject.transform.gameObject;
        WireMalePlug malePlug = go.GetComponentInParent<WireMalePlug>();
        Debug.Log(malePlug);

        if (malePlug == null)
        {
            Debug.LogWarning("No WireMalePlug found!");
            return;
        }

        if (malePlug.wireID == acceptedWireID)
        {
            isCorrect = true;
            Debug.Log("Correct wire connected: " + malePlug.wireID); 
        }
        else
        {
            isCorrect = false;
            Debug.Log("Wrong wire. Needed: " + acceptedWireID + ", got: " + malePlug.wireID);
        }
    }

    public void OnUnplug()
    {
        Debug.Log("Unplugged wire");
        isCorrect = false;
    }
}