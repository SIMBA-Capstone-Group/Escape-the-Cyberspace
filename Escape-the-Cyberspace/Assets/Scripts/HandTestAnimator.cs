using UnityEngine;

public class HandTestAnimator : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // Press G to make fist
        if (Input.GetKey(KeyCode.G))
            animator.SetFloat("Grip", 1f);
        else
            animator.SetFloat("Grip", 0f);
    }
}
