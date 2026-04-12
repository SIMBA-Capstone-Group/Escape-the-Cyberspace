using UnityEngine;

public class KeySnapTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform objectToRotate;   // assign Cube_10 here
    [SerializeField] private Transform keyTransform;     // assign the key here

    [Header("Rotation")]
    [SerializeField] private float rotateAmount = -90f;
    [SerializeField] private float rotateSpeed = 180f;

    [Header("Snap Check")]
    [SerializeField] private float requiredDistance = 0.05f; // how close key must be
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
        if (hasActivated || keyTransform == null || objectToRotate == null)
            return;

        float distance = Vector3.Distance(keyTransform.position, transform.position);

        if (distance <= requiredDistance)
        {
            hasActivated = true;
            shouldRotate = true;
            targetRotation = objectToRotate.rotation * Quaternion.Euler(0f, rotateAmount, 0f);
        }

        if (shouldRotate)
        {
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