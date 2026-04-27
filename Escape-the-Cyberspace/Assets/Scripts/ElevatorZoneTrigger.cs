using UnityEngine;

public class ManualElevatorCloser : MonoBehaviour
{
    [Header("The Real Doors (With Meshes)")]
    public Transform rightDoor;
    public Transform leftDoor;

    [Header("The Empty Objects (Your 'Target' Markers)")]
    public Transform rightDoorTarget;
    public Transform leftDoorTarget;

    [Header("Settings")]
    public float closeSpeed = 2f;
    public AudioSource audioSource;
    public AudioClip closeSound;

    private bool isClosing = false;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check for Player tag
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (audioSource && closeSound) audioSource.PlayOneShot(closeSound);
            
            isClosing = true;
            hasTriggered = true;
            Debug.Log("Player detected! Closing doors to local targets.");
        }
    }

    private void Update()
    {
        if (isClosing)
        {
            // Move using localPosition so they stay relative to the elevator
            rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, rightDoorTarget.localPosition, Time.deltaTime * closeSpeed);
            leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, leftDoorTarget.localPosition, Time.deltaTime * closeSpeed);

            // Check distance using localPosition
            if (Vector3.Distance(rightDoor.localPosition, rightDoorTarget.localPosition) < 0.01f)
            {
                rightDoor.localPosition = rightDoorTarget.localPosition;
                leftDoor.localPosition = leftDoorTarget.localPosition;
                isClosing = false;
                Debug.Log("Doors closed locally!");
            }
        }
    }
}