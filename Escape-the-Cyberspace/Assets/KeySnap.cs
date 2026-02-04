using UnityEngine;
using UnityEngine.Events;

public class KeySnap : MonoBehaviour
{
    [Header("Snap")]
    [SerializeField] private Transform snapPoint;
    [SerializeField] private Transform turnPivot;

    [Header("Detection")]
    [SerializeField] private string snapTag = "Snappable"; // Tag for the object to be snapped
    [SerializeField] private KeyCode turnKey = KeyCode.E; // for testing turning

    private float snapCooldown = 0.25f;

    [Header("Turn")]
    private float turnSpeed = 90f;
    private float maxTurnAngle = 90f;

    [Header("Next Level UI")]
    public UnityEvent onKeyInserted;
    public UnityEvent onUnlocked;

    private Transform insertedKey;
    private Rigidbody insertedRb;

    private bool inserted;
    private bool unlocked;
    private float turnedAngle;
    private float nextSnapTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Hit by: " + other.name + " | tag=" + other.tag);

        if (Time.time < nextSnapTime) return;
        if (inserted || unlocked) return;

        if(!other.CompareTag(snapTag)) 
        {
            Debug.Log("Hit object tag doesnt match. Expected: " + snapTag);
            return;
        }

        // Snap + lock it in
        insertedKey = other.transform;
        insertedRb = insertedKey.GetComponent<Rigidbody>();

        if (insertedRb == null)
        {
            Debug.LogWarning("Snapped object has no rigidBody. Add rigidbody to the object with the collider");
            return;
        }

        SnapKey();
        inserted = true;
        nextSnapTime = Time.time + snapCooldown;

        Debug.Log("Key inserted");
    }

    // Update is called once per frame
    private void Update()
    {
        if (!inserted || unlocked || insertedKey == null) return;

        // Turning input -- change
        if (Input.GetKey(turnKey))
        {
            float delta = turnSpeed * Time.deltaTime;
            float remaining = maxTurnAngle - turnedAngle;
            float applied = Mathf.Min(delta, remaining);

            insertedKey.RotateAround(turnPivot.position, turnPivot.up, applied);

            turnedAngle += applied;

            if(turnedAngle >= maxTurnAngle - 0.01f)
            {
                unlocked = true;
               
            }
        }
    }

    private void SnapKey()
    {
        if (insertedRb != null)
        {
            insertedRb.linearVelocity = Vector3.zero;
            insertedRb.angularVelocity = Vector3.zero;
            insertedRb.isKinematic = true; 
        }
        insertedKey.SetPositionAndRotation(snapPoint.position, snapPoint.rotation);

        insertedKey.SetParent(turnPivot, true);
    }
}
