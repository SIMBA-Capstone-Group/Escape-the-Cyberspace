using System.Collections;
using UnityEngine;

public class KeySnapTrigger : MonoBehaviour
{
    public Transform doorHinge;          // assign the whole door pivot here
    public Transform snapTarget;         // usually KeySnap itself
    public float snapDistance = 0.03f;   // how close key must be before it counts
    public float rotateDuration = 1f;

    private bool opened = false;
    private Collider currentKey;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        if (other.CompareTag("Key"))
        {
            currentKey = other;
            Debug.Log("Key entered trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentKey)
        {
            currentKey = null;
            Debug.Log("Key left trigger");
        }
    }

    private void Update()
    {
        if (opened || currentKey == null) return;

        float distance = Vector3.Distance(currentKey.transform.position, snapTarget.position);

        if (distance <= snapDistance)
        {
            Debug.Log("Key fully inserted");

            opened = true;

            // Snap key exactly into place
            currentKey.transform.position = snapTarget.position;
            currentKey.transform.rotation = snapTarget.rotation;

            Rigidbody rb = currentKey.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        Quaternion startRot = doorHinge.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, -90f, 0f);

        float t = 0f;

        while (t < rotateDuration)
        {
            t += Time.deltaTime;
            doorHinge.rotation = Quaternion.Slerp(startRot, endRot, t / rotateDuration);
            yield return null;
        }

        doorHinge.rotation = endRot;
    }
}