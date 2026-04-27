using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

        // We only loop up to the smallest list to prevent errors
        int notesToFill = Mathf.Min(stickyNotes.Count, passwordPools.Count);

        for (int i = 0; i < notesToFill; i++)
        {
            PasswordPool pool = passwordPools[i];
            TMP_Text note = stickyNotes[i];

            // Make sure the note exists and the pool actually has passwords inside it
            if (note != null && pool.passwords != null && pool.passwords.Count > 0)
            {
                // 1. Pick a random password from THIS specific pool
                int randomIndex = Random.Range(0, pool.passwords.Count);
                string selectedPassword = pool.passwords[randomIndex];

                // 2. Assign it directly to the matching sticky note
                note.text = selectedPassword;

                // 3. Turn on Auto Sizing via script
                note.enableAutoSizing = true;
                note.fontSizeMin = 2f;
                note.fontSizeMax = 26f;
            }
        }
    }
}