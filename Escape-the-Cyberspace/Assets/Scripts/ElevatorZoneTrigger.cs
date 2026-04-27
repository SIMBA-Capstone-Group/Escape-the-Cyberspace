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
        // VR Troubleshooting: Make sure your VR CameraRig or Hands have the "Player" tag!
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (audioSource && closeSound) audioSource.PlayOneShot(closeSound);
            
            isClosing = true;
            hasTriggered = true;
            Debug.Log("Player hit the trigger! Moving doors to targets.");
        }
    }

    private void Update()
    {
        if (isClosing)
        {
            // Move the real doors toward the positions of the Empty Object targets
            rightDoor.position = Vector3.Lerp(rightDoor.position, rightDoorTarget.position, Time.deltaTime * closeSpeed);
            leftDoor.position = Vector3.Lerp(leftDoor.position, leftDoorTarget.position, Time.deltaTime * closeSpeed);

            // Snap to position when close enough
            if (Vector3.Distance(rightDoor.position, rightDoorTarget.position) < 0.001f)
            {
                rightDoor.position = rightDoorTarget.position;
                leftDoor.position = leftDoorTarget.position;
                isClosing = false;
                Debug.Log("Doors fully closed.");
            }
        }
    }
}