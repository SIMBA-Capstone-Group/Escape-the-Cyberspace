using UnityEngine;

public class WireSocket : MonoBehaviour
{
    public string correctPlugID; // What SHOULD go here

    public WirePlug currentPlug;

    public delegate void OnConnectionChanged();
    public static event OnConnectionChanged ConnectionChanged;

    public void SnapIn(WirePlug plug)
    {
        currentPlug = plug;
        ConnectionChanged?.Invoke();
    }

    public void RemovePlug()
    {
        currentPlug = null;
        ConnectionChanged?.Invoke();
    }

    public bool IsCorrect()
    {
        if (currentPlug == null) return false;
        return currentPlug.plugID == correctPlugID;
    }
}