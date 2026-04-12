using System.Collections;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public Transform door;
    public Vector3 rotationAmount = new Vector3(0, -90, 0);
    public float rotateDuration = 1f;

    private bool hasOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (hasOpened) return;

        if (other.CompareTag("Key"))
        {
            Debug.Log("Key entered snap point");

            hasOpened = true;

            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            StartCoroutine(RotateDoorSmooth());
        }
    }

    IEnumerator RotateDoorSmooth()
    {
        Debug.Log("Starting door rotation");

        Quaternion startRotation = door.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAmount);

        float time = 0f;

        while (time < rotateDuration)
        {
            door.rotation = Quaternion.Slerp(startRotation, targetRotation, time / rotateDuration);
            time += Time.deltaTime;
            yield return null;
        }

        door.rotation = targetRotation;
        Debug.Log("Door rotation complete");
    }
}