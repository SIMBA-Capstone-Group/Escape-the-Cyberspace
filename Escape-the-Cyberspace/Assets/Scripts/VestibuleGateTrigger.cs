using UnityEngine;

public class VestibuleGateTriggerZone : MonoBehaviour
{
    public Doors entranceDoors;
    public Doors serverDoors;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered vestibule trigger!");
        Debug.Log(other);
        if (!other.CompareTag("Player")) return;
        
        if(entranceDoors.IsOpen())
        {
            Debug.Log("Closing entrance, opening server room...");
            entranceDoors.Close();
            serverDoors.Open();
        }
        else if(entranceDoors.IsClosed())
        {
            Debug.Log("Closing server room, opening entrance...");
            serverDoors.Close();
            entranceDoors.Open();
        }
    }
}