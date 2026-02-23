using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ShowUIOnGrab : MonoBehaviour
{
    public GameObject uiPopup; // Assign in Inspector

    [Header("Input")]
    [SerializeField] private UnityEngine.InputSystem.InputActionProperty unlockButton;

    [Header("Hand Interactors")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor leftInteractor;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor rightInteractor;

    [Header("Interaction Manager (assign or leave null to auto-find)")]
    [SerializeField] private XRInteractionManager interactionManager;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool isLocked = false;
    private bool isHeld = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor lockingInteractor;


    private bool isForcing;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if(interactionManager == null)
            interactionManager = FindFirstObjectByType<XRInteractionManager>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        unlockButton.action.Enable();
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);

        unlockButton.action.Disable();
    }

    private void Update()
    {
        if (isHeld && isLocked && unlockButton.action.WasPressedThisFrame())
        {
            Unlock();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (isForcing) return;

        isHeld = true;
        uiPopup.SetActive(true);

        // Checks which interactor grabs the cryptex
        lockingInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;

        LockToCurrentHand();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (isForcing) return;

        isHeld = false;

        if (!isLocked)
        {
            uiPopup.SetActive(false);
        }
    }

    private void LockToCurrentHand()
    {
        if (lockingInteractor == null || interactionManager == null) return;

        isLocked = true;

        if (!grabInteractable.isSelected)
        {
            isForcing = true;
            interactionManager.SelectEnter((UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor)lockingInteractor,(UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)grabInteractable);
            isForcing = false;
        }

        //Disable the interactor to prevent grabbing anything else
        lockingInteractor.enabled = false;

        Debug.Log($"Cryptex locked to: {lockingInteractor.transform.name}");
    }

    private void Unlock()
    {
        if (interactionManager == null) return;

        isLocked = false;

        if (lockingInteractor != null)
            lockingInteractor.enabled = true;

        // Force release from whichever interactor is holding it
        if (grabInteractable.isSelected && grabInteractable.interactorsSelecting.Count > 0)
        {
            var current = grabInteractable.interactorsSelecting[0];

            isForcing = true;
            interactionManager.SelectExit(current, grabInteractable);
            isForcing = false;
        }

        lockingInteractor = null;

        isHeld = false;
        uiPopup.SetActive(false);

        Debug.Log("Cryptex unlocked.");
    }
}