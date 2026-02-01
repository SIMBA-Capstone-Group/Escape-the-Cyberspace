using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class GrabRotateDisk : MonoBehaviour
{
    [Header("Rotation")]
    public Vector3 rotationAxis = Vector3.up; // local axis
    public float sensitivity = 1.0f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private Transform interactorTransform;

    private Vector3 lastProjectedVector;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Is Grabbed!");
        interactorTransform = args.interactorObject.transform;

        lastProjectedVector = ProjectToPlane(interactorTransform.position);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        interactorTransform = null;
    }

    void Update()
    {
        if (interactorTransform == null)
            return;

        Vector3 currentProjectedVector = ProjectToPlane(interactorTransform.position);

        float angle = Vector3.SignedAngle(
            lastProjectedVector,
            currentProjectedVector,
            transform.TransformDirection(rotationAxis)
        );

        transform.Rotate(
            rotationAxis,
            angle * sensitivity,
            Space.Self
        );

        lastProjectedVector = currentProjectedVector;
    }

    Vector3 ProjectToPlane(Vector3 worldPos)
    {
        Vector3 center = transform.position;
        Vector3 axisWorld = transform.TransformDirection(rotationAxis);

        Vector3 toHand = worldPos - center;

        return Vector3.ProjectOnPlane(toHand, axisWorld).normalized;
    }
}
