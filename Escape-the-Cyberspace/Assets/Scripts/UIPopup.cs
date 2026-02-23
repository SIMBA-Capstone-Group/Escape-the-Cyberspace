using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ShowUIOnGrab : MonoBehaviour
{
    public GameObject uiPopup; // Assign in Inspector

    [Header("Input")]
    [SerializeField] private UnityEngine.InputSystem.InputActionProperty lockButton;

    [Header("Hand Interactors")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor leftInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor rightInteractor;

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

        // STEP 1: Detect which hand grabbed it
        var grabbedBy = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;

        if (grabbedBy == leftInteractor)
            Debug.Log("Cryptex grabbed by LEFT hand");
        else if (grabbedBy == rightInteractor)
            Debug.Log("Cryptex grabbed by RIGHT hand");
        else
            Debug.Log($"Cryptex grabbed by OTHER interactor: {args.interactorObject.transform.name}");
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