using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CryptexRingRotate : MonoBehaviour
{

    
    [Header("Ring Settings")]
    public Transform ring;
    public float rotationSpeed = 120f;

    [Header("Input")]
    public InputActionProperty leftJoystick;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor currentInteractor;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnEnable()
    {
        leftJoystick.action.Enable();
    }

    void OnDisable()
    {
        leftJoystick.action.Disable();
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        currentInteractor = args.interactorObject;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        currentInteractor = null;
    }

    void Update()
    {

        if (currentInteractor == null)
            return;

        Vector2 joystick = leftJoystick.action.ReadValue<Vector2>();

        // Use X axis to rotate
        float rotationAmount = joystick.x * rotationSpeed * Time.deltaTime;
        ring.Rotate(Vector3.forward, rotationAmount, Space.Self);
    }
    
}
