using UnityEngine;
using TMPro;
using System.Collections.Generic;

// This creates a custom block in the Inspector where you can name the group and add its passwords
[System.Serializable]
public class PasswordPool
{
    [Tooltip("Name of the computer/group (e.g., 'Admin', 'Network Eng')")]
    public string poolName;

    [Tooltip("List of possible passwords for this specific computer")]
    public List<string> passwords;
}

public class PasswordNoteManager : MonoBehaviour
{
    [Header("All Sticky Notes in Scene")]
    [Tooltip("Drag all your sticky note TextMeshPro components here.")]
    public List<TMP_Text> stickyNotes;

    [Header("Dynamic Password Pools")]
    [Tooltip("Create your computer groups and their passwords here.")]
    public List<PasswordPool> passwordPools;

    void Start()
    {
        AssignPasswordsToNotes();
    }

    void AssignPasswordsToNotes()
    {
        if (stickyNotes.Count != passwordPools.Count)
        {
            Debug.LogWarning($"You have {stickyNotes.Count} sticky notes but {passwordPools.Count} password pools.");
        }

        List<string> chosenPasswords = new List<string>();

        foreach (PasswordPool pool in passwordPools)
        {
            if (pool.passwords != null && pool.passwords.Count > 0)
            {
                int randomIndex = Random.Range(0, pool.passwords.Count);
                chosenPasswords.Add(pool.passwords[randomIndex]);
            }
        }

        ShuffleList(chosenPasswords);

        int notesToFill = Mathf.Min(stickyNotes.Count, chosenPasswords.Count);

        for (int i = 0; i < notesToFill; i++)
        {
            if (stickyNotes[i] != null)
            {
                // 1. Assign the password
                stickyNotes[i].text = chosenPasswords[i];

                // 2. Turn on Auto Sizing via script
                stickyNotes[i].enableAutoSizing = true;

                // 3. Set your Min and Max font sizes (adjust these numbers to fit your specific VR scale)
                stickyNotes[i].fontSizeMin = 2f;
                stickyNotes[i].fontSizeMax = 26f;
            }
        }
    }

    // Helper function to shuffle the list
    void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}