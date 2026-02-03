using UnityEngine;
using TMPro;

public class UIButtonTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
  
    public void UpdateText()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI reference is missing!");
            return;
        }

        Debug.Log("Text: " + textMesh.text);
    }

    public void ScrollDown()
    {
        Debug.Log("Scrolling text down...");
        char curr = textMesh.text[0];
        char next = (char)(((curr - 64 + 26) % 26) + 65);
        textMesh.text = next.ToString();

    }

    public void ScrollUp()
    {
        Debug.Log("Scrolling text up...");
        char curr = textMesh.text[0];
        char next = (char)(((curr - 66 + 26) % 26) + 65);
        textMesh.text = next.ToString();
    }
}
