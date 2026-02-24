using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyboardPlacementCheck : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor KeyboardSnapPoint;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor ComputerPlug;
    public GameObject KeyboardUI;

    private void OnEnable()
    {
        KeyboardSnapPoint.selectEntered.AddListener(OnSnap);
        KeyboardSnapPoint.selectExited.AddListener(OnUnsnap);
    }

    private void OnDisable()
    {
        KeyboardSnapPoint.selectEntered.RemoveListener(OnSnap);
        KeyboardSnapPoint.selectExited.RemoveListener(OnUnsnap);
    }

    private void OnSnap(SelectEnterEventArgs args)
    {
        if(ComputerPlug.hasSelection == true)
        {
            KeyboardUI.SetActive(true);
        }
    }

    private void OnUnsnap(SelectExitEventArgs args)
    {
        KeyboardUI.SetActive(false);
    }
}
