using UnityEngine;

public class KeySnap : MonoBehaviour
{
    public string snapTag = "Snappable"; // Tag for the object to be snapped
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(snapTag))
        {
            
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
