using UnityEngine;

public class DisableColliderOnAccess : MonoBehaviour
{
    private Collider col;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void DisableCollision()
    {
        if (col != null)
            col.enabled = false;
    }
}
