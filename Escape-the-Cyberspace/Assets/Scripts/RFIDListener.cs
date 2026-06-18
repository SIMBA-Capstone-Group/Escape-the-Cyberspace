using System;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RFIDData
{
    public int id;
    public string ownerName;
    public string role;
}

[System.Serializable]
public class RFIDDatabase
{
    public RFIDData[] cards;
}

public class RFIDListener : MonoBehaviour
{
    // --- RFID Cloner stuff ---
    public TextMeshProUGUI employeeName;
    protected RFIDData currentEmployee;

    public RFIDData GetCurrentEmployee()
    {
        return currentEmployee;
    }

    // --- Computer Stuff ---
    private RFIDDatabase potentialRFIDDatabase;
    public TextAsset potentialRFIDContentsFile;
    public Transform container;
    public GameObject cardObject;

    // --- RFID Tag stuff ---
    public GameObject rFIDTag;
    protected string rFIDTagContents = "NONE";

    // --- Door Lock stuff ---
    protected int correctRFID; // Default/Global correct ID (Victor)

    public int GetCorrectRFID()
    {
        return correctRFID;
    }
    // NEW: This is the "Job Check" loop you asked for!
    // This allows any scanner to ask: "Does this ID match this Job Role?"
    public bool CheckAccessByRole(int scannedID, string requiredRole)
    {
        if (potentialRFIDDatabase == null) return false;

        foreach (var card in potentialRFIDDatabase.cards)
        {
            // If we find the person who scanned...
            if (card.id == scannedID)
            {
                // ...check if their role matches what the scanner wants
                if (card.role == requiredRole)
                {
                    Debug.Log($"Access Granted: {card.ownerName} matches role {requiredRole}");
                    return true;
                }
            }
        }

        Debug.Log("Access Denied: Role mismatch.");
        return false;
    }

    void GenerateCards()
    {
        foreach (var card in potentialRFIDDatabase.cards)
        {
            GameObject cardUI = Instantiate(cardObject, container, false);
            TextMeshProUGUI textComponent = cardUI.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                cardUI.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.ownerName;
                cardUI.transform.Find("Position").GetComponent<TextMeshProUGUI>().text = card.role;
            }

            Button btn = cardUI.GetComponentInChildren<Button>();
            if (btn != null)
            {
                RFIDData capturedCard = card;
                btn.onClick.AddListener(() => OnCardButtonClicked(capturedCard));
            }
        }
    }

    void OnCardButtonClicked(RFIDData capturedCard)
    {
        currentEmployee = capturedCard;
        employeeName.text = capturedCard.ownerName;
    }
    void Start()
    {
        if (potentialRFIDContentsFile != null)
        {
            potentialRFIDDatabase = JsonUtility.FromJson<RFIDDatabase>(potentialRFIDContentsFile.text);
            Debug.Log("Loaded cards count: " + potentialRFIDDatabase.cards.Length);

            foreach (var card in potentialRFIDDatabase.cards)
            {
                // This still sets the default "Victor" ID for your original GateAccessMachine
                if (card.role == "Security Engineer")
                {
                    correctRFID = card.id;
                    Debug.Log("Default Security Engineer ID set to: " + correctRFID);
                }
            }
        }
        else
        {
            Debug.LogError("JSON file not found!");
        }
        GenerateCards();
    }
}


