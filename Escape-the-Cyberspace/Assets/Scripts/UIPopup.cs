using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowUIOnGrab : MonoBehaviour
{
    public GameObject uiPopup; // Assign in Inspector
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        uiPopup.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        uiPopup.SetActive(false);
    }
}