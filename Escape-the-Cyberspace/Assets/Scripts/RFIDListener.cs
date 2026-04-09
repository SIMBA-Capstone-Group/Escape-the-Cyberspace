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
    // RFID Cloner stuff
    public TextMeshProUGUI employeeName;
    protected RFIDData currentEmployee;
    public RFIDData GetCurrentEmployee()
    {
        return currentEmployee;
    }

    // Computer Stuff
    private RFIDDatabase potentialRFIDDatabase;
    public TextAsset potentialRFIDContentsFile;
    public Transform container;
    public GameObject cardObject;
    
    // RFID Tag stuff
    public GameObject rFIDTag;
    protected string rFIDTagContents = "NONE";

    // Door Lock stuff
    protected int correctRFID;
    public int GetCorrectRFID()
    {
        return correctRFID;
    }
    


    void GenerateCards()
    {
        foreach (var card in potentialRFIDDatabase.cards)
        {
            // Instantiate prefab
            GameObject cardUI = Instantiate(cardObject, container);

            // Set Text
            TextMeshProUGUI textComponent = cardUI.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                cardUI.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.ownerName;
                cardUI.transform.Find("Position").GetComponent<TextMeshProUGUI>().text = card.role;
            }

            // Set Button callback
            Button btn = cardUI.GetComponentInChildren<Button>();
            if (btn != null)
            {
                // Capture local variable to avoid closure issues
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

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Load JSON file of employees and their IDs
        if (potentialRFIDContentsFile != null)
        {
            potentialRFIDDatabase = JsonUtility.FromJson<RFIDDatabase>(potentialRFIDContentsFile.text);
            Debug.Log("Loaded cards count: " + potentialRFIDDatabase.cards.Length);
            foreach (var card in potentialRFIDDatabase.cards)
            {
                Debug.Log($"ID: {card.id}, Name: {card.ownerName}, Role: {card.role}");
                if(card.role == "Security Engineer")
                {
                    correctRFID = card.id;
                    Debug.Log("Correct personel found");
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
