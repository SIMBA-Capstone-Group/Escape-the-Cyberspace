using UnityEngine;

public class ElevatorPower : MonoBehaviour
{
    [Tooltip("Drag your Card Scanner object into this slot in the Inspector")]
    public Collider cardScannerCollider;

    void Start()
    {
        // This ensures the scanner is turned off the moment the game starts
        if (cardScannerCollider != null)
        {
            cardScannerCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("Card Scanner Collider is not assigned in the Computer Terminal script!");
        }
    }

    // This is the function we will trigger with the UI Button
    public void RestoreElevatorPower()
    {
        if (cardScannerCollider != null)
        {
            cardScannerCollider.enabled = true;
            Debug.Log("Power restored! The card scanner is now active.");
        }
    }
}