using UnityEngine;

public class ManualElevatorCloser : MonoBehaviour
{
    [Header("The Real Doors")]
    public Transform rightDoorTransform;
    public Transform leftDoorTransform;

    [Header("Movement (Match your original script)")]
    public Vector3 rightOpenOffset = new Vector3(0, 0, 2f); // Example: 2 on Z
    public Vector3 leftOpenOffset = new Vector3(0, 0, -2f);
    public float closeSpeed = 2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip closeSound;

    private Vector3 rightClosedPosition;
    private Vector3 leftClosedPosition;
    private bool isClosing = false;
    private bool hasTriggered = false;

    void Start()
    {
        // AS LONG AS THE DOORS ARE CLOSED WHEN YOU PRESS PLAY:
        // This records their exact location in the world right now.
        rightClosedPosition = rightDoorTransform.position;
        leftClosedPosition = leftDoorTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (audioSource && closeSound) audioSource.PlayOneShot(closeSound);
            
            isClosing = true;
            hasTriggered = true;
            Debug.Log("Player entered! Returning doors to saved world positions.");
        }
    }

    void Update()
    {
        if (isClosing)
        {
            // Move back to the positions recorded at Start()
            rightDoorTransform.position = Vector3.Lerp(rightDoorTransform.position, rightClosedPosition, Time.deltaTime * closeSpeed);
            leftDoorTransform.position = Vector3.Lerp(leftDoorTransform.position, leftClosedPosition, Time.deltaTime * closeSpeed);

            // Snap shut and stop script
            if (Vector3.Distance(rightDoorTransform.position, rightClosedPosition) < 0.01f)
            {
                rightDoorTransform.position = rightClosedPosition;
                leftDoorTransform.position = leftClosedPosition;
                isClosing = false;
                Debug.Log("Elevator doors successfully returned to closed position.");
            }
        }
    }
}