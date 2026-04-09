using Unity.VisualScripting;
using UnityEngine;

public class GateAccessMachine : MonoBehaviour
{
    public RFIDListener listener;
    public Doors doors;

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
                doors.Open();
            }
            else
            {
                Debug.Log("Access denied!");
            }
        }
    }
}
