using UnityEngine;

public class SnappingLid : MonoBehaviour
{
    public Transform snapAnchor;
    public Transform snapPoint;

    public float snapDistance = 0.1f;
    public float snapSpeed = 12f;

    public float breakDistance = 0.15f;

    public bool isSnapped = false;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isSnapped)
        {
            float dist = Vector3.Distance(
                snapAnchor.position,
                snapPoint.position
            );

            if (dist <= snapDistance)
            {
                Snap();
            }
        }
        else
        {
            float dist = Vector3.Distance(
                snapAnchor.position,
                snapPoint.position
            );

            if (dist > breakDistance)
            {
                Unsnap();
            }
        }
    }

    void Snap()
    {
        isSnapped = true;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Unsnap()
    {
        isSnapped = false;
        rb.isKinematic = false;
    }

   void Update()
    {
        if (!isSnapped)
            return;

        // Calculate offset needed to align anchor with snap point
        Vector3 positionOffset = snapPoint.position - snapAnchor.position;
        transform.position += positionOffset;

        // Match rotation while preserving anchor alignment
        Quaternion rotationOffset =
            snapPoint.rotation * Quaternion.Inverse(snapAnchor.rotation);

        transform.rotation = rotationOffset * transform.rotation;
    }
}
