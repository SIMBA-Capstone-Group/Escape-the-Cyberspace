using UnityEngine;
using UnityEngine;

public class SnapToPoint : MonoBehaviour
{
    public Transform snapAnchor;
    public Transform snapPoint;

    public float snapDistance = 0.1f;
    public float snapSpeed = 12f;

    public bool isSnapped = false;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isSnapped || snapAnchor == null || snapPoint == null)
            return;

        float dist = Vector3.Distance(
            snapAnchor.position,
            snapPoint.position
        );

        if (dist <= snapDistance)
        {
            StartSnap();
        }
    }

    void StartSnap()
    {
        isSnapped = true;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (!isSnapped)
            return;

        transform.position = Vector3.Lerp(
            transform.position,
            snapPoint.position,
            Time.deltaTime * snapSpeed
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            snapPoint.rotation,
            Time.deltaTime * snapSpeed
        );
    }
}
