using UnityEngine;
using TMPro;
using System.Diagnostics.CodeAnalysis;

public class ChalkboardPuzzle : MonoBehaviour
{
    public TMP_Text messageBox;
    public TMP_Text rotationBox;

    private string[] messagePossibilities = {"Check my computer", "Go to the computer", "On my computer", "Get the computer"};

    private string caesaredMessage(string plaintext, int rotation)
    {
        string ciphertext = "";
        foreach (char c in plaintext)
        {
            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                int shifted = ((c - offset + rotation) % 26 + 26) % 26;
                ciphertext += (char)(shifted + offset);
            }
            else
            {
                ciphertext += c;
            }
        }

        return ciphertext.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int rotation = Random.Range(4, 18);
        int message = Random.Range(0, messagePossibilities.Length);
        rotationBox.text = rotation.ToString();
        messageBox.text = caesaredMessage(messagePossibilities[message], rotation);
    }
}
