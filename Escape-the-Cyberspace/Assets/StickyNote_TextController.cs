using UnityEngine;
using TMPro;

public class TMPCharController : MonoBehaviour
{
    [Header("TMP Text References (1 char each)")]
    public TMP_Text char1;
    public TMP_Text char2;
    public TMP_Text char3;
    public TMP_Text char4;
    public TMP_Text char5;

    [Header("Character Values")]
    public string value1;
    public string value2;
    public string value3;
    public string value4;
    public string value5;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        char1.text = Sanitize(value1);
        char2.text = Sanitize(value2);
        char3.text = Sanitize(value3);
        char4.text = Sanitize(value4);
        char5.text = Sanitize(value5);
    }

    string Sanitize(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        return input.Substring(0, 1);
    }
}