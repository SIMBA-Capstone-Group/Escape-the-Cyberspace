using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform rightDoorTransform;
    public Vector3 rightOpenOffset = new Vector3(0, 0, 0);
    public Transform leftDoorTransform;
    public Vector3 leftOpenOffset = new Vector3(0, 0, 0);
    public float openSpeed = 2f;

    // Audio
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip openSound;    
    public AudioClip closeSound;   

    private Vector3 rightClosedPosition;
    private Vector3 leftClosedPosition;
    private Vector3 rightOpenPosition;
    private Vector3 leftOpenPosition;
    private bool isOpening = false;
    protected bool isOpen = false;
    private bool isClosing = false;
    protected bool isClosed = true;

    void Start()
    {
        rightClosedPosition = rightDoorTransform.position;
        leftClosedPosition = leftDoorTransform.position;
        rightOpenPosition = rightClosedPosition + rightOpenOffset;
        leftOpenPosition = leftClosedPosition + leftOpenOffset;

        // Validation check to avoid errors if you forget to assign the AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isOpening)
        {
            rightDoorTransform.position = Vector3.Lerp(rightDoorTransform.position, rightOpenPosition, Time.deltaTime * openSpeed);
            leftDoorTransform.position = Vector3.Lerp(leftDoorTransform.position, leftOpenPosition, Time.deltaTime * openSpeed);

            if (Vector3.Distance(rightDoorTransform.position, rightOpenPosition) < 0.01f)
            {
                rightDoorTransform.position = rightOpenPosition;
                leftDoorTransform.position = leftOpenPosition;
                isOpening = false;
            }
        }
        else if (isClosing)
        {
            rightDoorTransform.position = Vector3.Lerp(rightDoorTransform.position, rightClosedPosition, Time.deltaTime * openSpeed);
            leftDoorTransform.position = Vector3.Lerp(leftDoorTransform.position, leftClosedPosition, Time.deltaTime * openSpeed);

            if (Vector3.Distance(rightDoorTransform.position, rightClosedPosition) < 0.01f)
            {
                rightDoorTransform.position = rightClosedPosition;
                leftDoorTransform.position = leftClosedPosition;
                isClosing = false;
            }
        }
    }

    public void Open()
    {
        if(IsOpen())
        {
            Debug.Log("Door already open!");
            return;
        }

        // Audio
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        isOpening = true;
        isClosing = false;
        Debug.Log("Door opening!");
        isOpen = true;
        isClosed = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void Close()
    {
        if(IsClosed())
        {
            Debug.Log("Door already closed!");
            return;
        }

        // Audio
        if (audioSource != null && closeSound != null)
        {
            audioSource.PlayOneShot(closeSound);
        }

        isClosing = true;
        isOpening = false;
        Debug.Log("Door closing!");
        isClosed = true;
        isOpen = false;
    }

    public bool IsClosed()
    {
        return isClosed;
    }
}