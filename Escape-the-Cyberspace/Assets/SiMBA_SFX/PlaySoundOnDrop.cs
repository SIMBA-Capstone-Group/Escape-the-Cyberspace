using UnityEngine;

public class PlaySoundOnDrop : MonoBehaviour
{
    // floor object and audio
    public GameObject floorObject; 
    public AudioSource audioSource;
   
    // boolean for if sound has played already
    private bool hasPlayed = false;
    
    void Start()
    {
    }

    // if cryptex hits the floor
    private void OnCollisionEnter(Collision collision)
    {
        
	// plays audio if it collides with the floor object for the first time
        if (collision.gameObject == floorObject && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
            
            // stops script
            this.enabled = false; 
        }
    }
}