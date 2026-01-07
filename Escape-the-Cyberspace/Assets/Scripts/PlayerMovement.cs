using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 2f;
    public float gravity = -9.8f;

    CharacterController controller;
    Animator animator;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        animator.SetFloat("Speed", move.magnitude);

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
