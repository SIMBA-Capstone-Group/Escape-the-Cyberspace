using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorInput : MonoBehaviour
{
    public Animator animator;

    // This will come from the XR Controller
    public InputActionProperty gripAction;

    void Update()
    {
        if (gripAction.action != null)
        {
            float gripValue = gripAction.action.ReadValue<float>();
            animator.SetFloat("Grip", gripValue);
        }
    }
}