using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ShowUIOnGrab : MonoBehaviour
{
    public GameObject uiPopup; // Assign in Inspector

    [Header("Input")]
    [SerializeField] private UnityEngine.InputSystem.InputActionProperty lockButton;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool isLocked = false;
    private bool isHeld = false;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        lockButton.action.Enable();
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);

        lockButton.action.Disable();
    }

    private void Update()
    {
        if (isHeld && lockButton.action.WasPressedThisFrame())
        {
            ToggleLock();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        uiPopup.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        
        uiPopup.SetActive(false);
    }

    private void ToggleLock()
    {
        isLocked = !isLocked;

         if (isLocked)
        {
            Debug.Log("Cryptex Locked");
        }
        else
        {
            Debug.Log("Cryptex Unlocked");
        }
    }
}