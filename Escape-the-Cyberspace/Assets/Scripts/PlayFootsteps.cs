using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    private AudioSource footstepsSound;
    private CharacterController characterController;

    void Start()
    {
        footstepsSound = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController != null && footstepsSound != null)
        {
            if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
            {
                footstepsSound.enabled = true;
            }
            else
            {
                footstepsSound.enabled = false;
            }
        }
    }
}