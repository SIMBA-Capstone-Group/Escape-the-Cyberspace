using UnityEngine;

public class KeySnapTrigger : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform cubeToRotate;

    [Header("Rotation Settings")]
    [SerializeField] private float rotateAmount = -90f;
    [SerializeField] private float rotateSpeed = 180f;

    [Header("Key Settings")]
    [SerializeField] private string requiredLayerName = "KeySnap";

    private bool shouldRotate = false;
    private bool hasActivated = false;
    private Quaternion targetRotation;

    private void Start()
    {
        if (cubeToRotate != null)
        {
            targetRotation = cubeToRotate.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated || cubeToRotate == null) return;

        // Check if the object entering is on the required layer
        if (other.gameObject.layer == LayerMask.NameToLayer(requiredLayerName))
        {
            hasActivated = true;
            shouldRotate = true;
            targetRotation = cubeToRotate.rotation * Quaternion.Euler(0f, rotateAmount, 0f);
        }
    }

    private void Update()
    {
        if (!shouldRotate || cubeToRotate == null) return;

        cubeToRotate.rotation = Quaternion.RotateTowards(
            cubeToRotate.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(cubeToRotate.rotation, targetRotation) < 0.1f)
        {
            cubeToRotate.rotation = targetRotation;
            shouldRotate = false;
        }
    }
}
