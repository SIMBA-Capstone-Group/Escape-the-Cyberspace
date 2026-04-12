using UnityEngine;

public class KeySnapTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private Transform keyTransform;

    [Header("Rotation")]
    [SerializeField] private float rotateAmount = -90f;
    [SerializeField] private float rotateSpeed = 180f;

    [Header("Snap Check")]
    [SerializeField] private float requiredDistance = 0.05f;
    [SerializeField] private string requiredTag = "Key";

    private bool shouldRotate = false;
    private bool hasActivated = false;
    private Quaternion targetRotation;

    private void Start()
    {
        if (objectToRotate != null)
            targetRotation = objectToRotate.rotation;
    }

    private void Update()
    {
        if (keyTransform == null || objectToRotate == null)
            return;

        Debug.Log("Distance: " + Vector3.Distance(keyTransform.position, transform.position));

if (!hasActivated)
{
    float distance = Vector3.Distance(keyTransform.position, transform.position);

    if (distance <= requiredDistance && keyTransform.CompareTag(requiredTag))
    {
        Debug.Log("Key inserted, starting rotation");
        hasActivated = true;
        shouldRotate = true;
        targetRotation = objectToRotate.rotation * Quaternion.Euler(0f, rotateAmount, 0f);
    }
}

        // Continue rotating until target is reached
        if (shouldRotate)
        {
            Debug.Log("Rotating door...");
            objectToRotate.rotation = Quaternion.RotateTowards(
                objectToRotate.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(objectToRotate.rotation, targetRotation) < 0.1f)
            {
                objectToRotate.rotation = targetRotation;
                shouldRotate = false;
            }
        }
    }
}