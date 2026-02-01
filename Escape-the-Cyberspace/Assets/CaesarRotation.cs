using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRRotatableRing : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Transform rotationCenter; // Center of the Caesar disk
    public Vector3 rotationAxis = Vector3.right; // X-axis rotation

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor grabbingHand;
    private bool isGrabbed = false;

    private Vector3 lastHandDirection;

    private void OnEnable()
    {
        var grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogWarning("VRRotatableRing requires XRGrabInteractable on the same GameObject!");
        }
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        grabbingHand = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;
        isGrabbed = true;

        // Initial vector from center to hand
        lastHandDirection = (grabbingHand.transform.position - rotationCenter.position).normalized;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        grabbingHand = null;
        isGrabbed = false;
    }

    private void Update()
    {
        if (isGrabbed && grabbingHand != null)
        {
            Vector3 currentHandDirection = (grabbingHand.transform.position - rotationCenter.position).normalized;

            // Calculate angle relative to X-axis
            float angle = Vector3.SignedAngle(lastHandDirection, currentHandDirection, rotationAxis);

            // Apply rotation around X-axis
            transform.Rotate(rotationAxis, angle, Space.World);

            lastHandDirection = currentHandDirection;
        }
    }
}
