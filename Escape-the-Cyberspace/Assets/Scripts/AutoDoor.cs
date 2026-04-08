using UnityEngine;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorHinge; // Drag your door (or hinge parent) here in the inspector
    public float openAngle = 90f; // Degrees the door should open
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Save the starting rotation and calculate the target open rotation
        closedRotation = doorHinge.rotation;
        openRotation = Quaternion.Euler(doorHinge.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            StopAllCoroutines();
            StartCoroutine(SwingDoor(openRotation));
        }
    }

    // Optional: Close the door when the player leaves
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            isOpen = false;
            StopAllCoroutines();
            StartCoroutine(SwingDoor(closedRotation));
        }
    }

    IEnumerator SwingDoor(Quaternion targetRotation)
    {
        float time = 0;
        Quaternion startRotation = doorHinge.rotation;

        while (time < 1)
        {
            // Smoothly rotate from the current rotation to the target
            doorHinge.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            time += Time.deltaTime * openSpeed;
            yield return null;
        }

        // Ensure it snaps exactly to the target at the end
        doorHinge.rotation = targetRotation;
    }
}