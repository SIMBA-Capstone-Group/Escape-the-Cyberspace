using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor))]
public class WireFemaleSocket : MonoBehaviour
{
    public string acceptedWireID;
    public Transform snapPoint;
    public UnityEvent onCorrectWirePlugged;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private bool isConnected = false;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnPluggedIn);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnPluggedIn);
    }

    private void OnPluggedIn(SelectEnterEventArgs args)
    {
        Debug.Log("Plugged in!");
        if (isConnected) return;

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
            isConnected = true;

            // if (snapPoint != null)
            // {
            //     malePlug.transform.position = snapPoint.position;
            //     malePlug.transform.rotation = snapPoint.rotation;
            // }

            //onCorrectWirePlugged.Invoke();

            Debug.Log("Correct wire connected: " + malePlug.wireID); 
        }
        else
        {
            Debug.Log("Wrong wire. Needed: " + acceptedWireID + ", got: " + malePlug.wireID);
        }
    }
}