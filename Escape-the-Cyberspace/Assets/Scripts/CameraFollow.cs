using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothPosition;
        transform.LookAt(target);
    }
}
