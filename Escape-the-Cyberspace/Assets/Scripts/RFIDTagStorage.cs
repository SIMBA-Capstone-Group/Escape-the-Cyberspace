using UnityEngine;

public class RFIDTag : MonoBehaviour
{
    public int storedID = -1;

    public void SetID(int newID)
    {
        storedID = newID;
        Debug.Log("Tag updated to ID: " + storedID);
    }
}