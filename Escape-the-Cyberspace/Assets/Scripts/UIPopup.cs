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

    private InteractionLayerMask leftOriginalMask;
    private InteractionLayerMask rightOriginalMask;

    private void Awake()
    {
        leftOriginalMask = leftInteractor.interactionLayers;
        rightOriginalMask = rightInteractor.interactionLayers;
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

        if (isLocked && lockingInteractor != null)
        {
            // Re-grab immediately if player tries to release while locked
            isForcing = true;
            interactionManager.SelectEnter(
                (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor)lockingInteractor,
                (UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)grabInteractable
            );
            isForcing = false;
            return;
        }

        isHeld = false;
        uiPopup.SetActive(false);
    }

    private void LockToCurrentHand()
    {
        if (lockingInteractor == null || interactionManager == null) return;

        isLocked = true;

        // Restrict the grabbing hand so it can only select Cryptex-layer objects
        // (keeps rays/UI working because the interactor stays enabled)
        var cryptexOnly = grabInteractable.interactionLayers;

        if (lockingInteractor == leftInteractor)
            leftInteractor.interactionLayers = cryptexOnly;
        else if (lockingInteractor == rightInteractor)
            rightInteractor.interactionLayers = cryptexOnly;

        Debug.Log($"Cryptex locked to: {lockingInteractor.transform.name}");
    }

    private System.Collections.IEnumerator DisableInteractorNextFrame(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        yield return null; // wait one frame
        interactor.enabled = false;
    }

    private void Unlock()
    {
        if (interactionManager == null) return;

        isLocked = false;

        // Restore interaction masks so hands can grab everything again
        leftInteractor.interactionLayers = leftOriginalMask;
        rightInteractor.interactionLayers = rightOriginalMask;

        // Force release from whichever interactor is holding it
        if (grabInteractable.isSelected && grabInteractable.interactorsSelecting.Count > 0)
        {
            var current = grabInteractable.interactorsSelecting[0];

            isForcing = true;
            interactionManager.SelectExit(
                (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor)current,
                (UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)grabInteractable
            );
            isForcing = false;
        }

        lockingInteractor = null;
        isHeld = false;
        uiPopup.SetActive(false);

        Debug.Log("Cryptex unlocked.");
    }
}