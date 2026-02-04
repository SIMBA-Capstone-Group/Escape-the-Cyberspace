using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeypadButtonXRHook : MonoBehaviour
{
    [SerializeField] private NavKeypad.KeypadButton keypadButton;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Reset()
    {
        keypadButton = GetComponent<NavKeypad.KeypadButton>();
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    private void OnEnable()
    {
        if (!interactable) interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (keypadButton != null)
        keypadButton.PressButton();
    }
}
