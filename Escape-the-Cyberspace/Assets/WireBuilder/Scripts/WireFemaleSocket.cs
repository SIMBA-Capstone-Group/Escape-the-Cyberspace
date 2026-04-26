using UnityEngine;
using UnityEngine.Events;

public class WireFemaleSocket : MonoBehaviour
{
    public string acceptedWireID;
    public Transform snapPoint;
    public UnityEvent onCorrectWirePlugged;

    private bool isConnected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isConnected) return;

        WireMalePlug malePlug = other.GetComponent<WireMalePlug>();

        if (malePlug == null)
        {
            return;
        }

        if (malePlug.wireID == acceptedWireID)
        {
            isConnected = true;

            Rigidbody rb = malePlug.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            if (snapPoint != null)
            {
                malePlug.transform.position = snapPoint.position;
                malePlug.transform.rotation = snapPoint.rotation;
            }
            else
            {
                malePlug.transform.position = transform.position;
                malePlug.transform.rotation = transform.rotation;
            }

            onCorrectWirePlugged.Invoke();

            Debug.Log("Correct wire connected: " + malePlug.wireID);
        }
        else
        {
            Debug.Log("Wrong wire. Needed: " + acceptedWireID + ", got: " + malePlug.wireID);
        }
    }
}