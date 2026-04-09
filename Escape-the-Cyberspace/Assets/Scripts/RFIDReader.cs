using TMPro;
using UnityEngine;
using System.Collections;

public class RFIDReader : MonoBehaviour
{
    public RFIDTag currentTag;
    private bool isScanning = false;
    public Collider rFIDScanDistance;
    public RFIDListener listener;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {

        
        Debug.Log("Something entered trigger: " + other.name);

        RFIDTag tag = other.GetComponentInParent<RFIDTag>();

        if (tag != null && !isScanning)
        {
            Debug.Log("Tag entered - starting scan...");
            currentTag = tag;
            StartCoroutine(ScanProcess());
        }





        if (tag != null)
        {
            Debug.Log("RFID Tag detected in cloner area!");
            var employee = listener.GetCurrentEmployee();

            // Only clone if you have selected an employee
            if (employee != null)
            {
                tag.SetID(employee.id);
                Debug.Log("Cloned ID: " + employee.id);
            }
            else
            {
                Debug.Log("No employee selected!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RFIDTag tag = other.GetComponentInParent<RFIDTag>();

        if (tag != null && tag == currentTag && isScanning)
        {
            Debug.Log("Tag removed - cancelling scan.");
            isScanning = false;
            currentTag = null;

            // Reset UI
            listener.employeeName.text = "Scan Cancelled";
        }
    }


    IEnumerator ScanProcess()
    {
        isScanning = true;

        // Show "Updating..."
        listener.employeeName.text = "Updating...";

        float scanTime = 2.5f;
        float timer = 0f;

        while (timer < scanTime)
        {
            // If tag was removed → cancel
            if (!isScanning || currentTag == null)
            {
                yield break;
            }

            timer += Time.deltaTime;
            listener.employeeName.text = $"Updating... {(int)(timer / scanTime * 100)}%";

            yield return null;
        }

        // Scan complete
        var employee = listener.GetCurrentEmployee();

        if (employee != null && currentTag != null)
        {
            currentTag.SetID(employee.id);
            listener.employeeName.text = employee.ownerName;
            Debug.Log("Clone complete!");
        }
        else
        {
            listener.employeeName.text = "No Data";
        }

        isScanning = false;
    }
}
