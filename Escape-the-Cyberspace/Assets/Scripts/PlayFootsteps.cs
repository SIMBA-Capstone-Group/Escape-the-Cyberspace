using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    private AudioSource footstepsSound;
    private Vector3 lastPosition;

    // This defines how far the player must move per frame to trigger the sound.
    // 0.005f is small enough to catch slow walking, but large enough to ignore VR headset jitter.
    private float movementThreshold = 0.005f;

    void Start()
    {
        footstepsSound = GetComponent<AudioSource>();

        // Record our starting position the second the game begins
        lastPosition = transform.position;
    }

    void Update()
    {
        if (footstepsSound != null)
        {
            // Get our current position
            Vector3 currentPosition = transform.position;

            // Calculate how much we moved on the X and Z axes (ignoring Y so moving your head up/down doesn't trigger footsteps)
            float distanceMoved = Vector2.Distance(
                new Vector2(currentPosition.x, currentPosition.z),
                new Vector2(lastPosition.x, lastPosition.z)
            );

            // If we moved more than the threshold, play the sound
            if (distanceMoved > movementThreshold)
            {
                if (!footstepsSound.isPlaying)
                {
                    footstepsSound.Play();
                }
            }
            else
            {
                // Pause instead of Stop so the footstep audio doesn't restart from the very beginning every single time you tap the joystick
                footstepsSound.Pause();
            }

            // Save our current position to compare against in the next frame
            lastPosition = currentPosition;
        }
    }
}