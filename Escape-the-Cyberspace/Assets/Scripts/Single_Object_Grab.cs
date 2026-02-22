sing UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaySingleGrab : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
            if (interactable != null && !interactable.isSelected)
            {
                // Only grab the closest object
                rayInteractor.StartManualInteraction(interactable);
            }
        }
    }
}