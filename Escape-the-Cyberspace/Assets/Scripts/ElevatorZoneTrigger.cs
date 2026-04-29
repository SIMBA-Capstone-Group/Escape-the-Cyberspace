using UnityEngine;

public class ManualElevatorCloser : MonoBehaviour
{
    [Header("The Real Doors")]
    public Transform rightDoorTransform;
    public Transform leftDoorTransform;

    [Header("Movement")]
    public float closeSpeed = 2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip closeSound;

    [Header("Level Complete UI")]
    public GameObject levelCompleteUI;

    [Header("Player Control")]
    public MonoBehaviour playerMovementScript; // drag your movement script here

    private Vector3 rightClosedPosition;
    private Vector3 leftClosedPosition;
    private bool isClosing = false;
    private bool hasTriggered = false;

    void Start()
    {
        rightClosedPosition = rightDoorTransform.position;
        leftClosedPosition = leftDoorTransform.position;

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            if (audioSource && closeSound)
                audioSource.PlayOneShot(closeSound);

            isClosing = true;
            hasTriggered = true;
        }
    }

    void Update()
    {
        if (isClosing)
        {
            rightDoorTransform.position = Vector3.Lerp(
                rightDoorTransform.position,
                rightClosedPosition,
                Time.deltaTime * closeSpeed
            );

            leftDoorTransform.position = Vector3.Lerp(
                leftDoorTransform.position,
                leftClosedPosition,
                Time.deltaTime * closeSpeed
            );

            if (Vector3.Distance(rightDoorTransform.position, rightClosedPosition) < 0.01f &&
                Vector3.Distance(leftDoorTransform.position, leftClosedPosition) < 0.01f)
            {
                rightDoorTransform.position = rightClosedPosition;
                leftDoorTransform.position = leftClosedPosition;

                isClosing = false;

                // Show UI
                if (levelCompleteUI != null)
                    levelCompleteUI.SetActive(true);

                // 🚫 Disable player movement
                if (playerMovementScript != null)
                    playerMovementScript.enabled = false;

                Debug.Log("Doors closed. Player frozen. UI shown.");
            }
        }
    }
}