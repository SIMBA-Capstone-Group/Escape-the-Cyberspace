using UnityEngine;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorHinge;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = doorHinge.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            StopAllCoroutines();
            StartCoroutine(SwingDoor(openRotation));
        }
    }

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
        float t = 0f;
        Quaternion startRotation = doorHinge.localRotation;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            doorHinge.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        doorHinge.localRotation = targetRotation;
    }
}