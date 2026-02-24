using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutputOfCryptex : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private Image flashTarget;          // background panel OR overlay image
    [SerializeField] private TMP_Text incorrectText;     // "INCORRECT" text

    [Header("Tuning")]
    [SerializeField] private float flashOnTime = 0.12f;
    [SerializeField] private float flashOffTime = 0.10f;
    [SerializeField] private int flashCount = 2;

    [SerializeField] private float incorrectShowTime = 1.0f;

    private Color originalFlashColor;
    private Coroutine routine;

    private void Awake()
    {
        if (flashTarget != null)
            originalFlashColor = flashTarget.color;

        if (incorrectText != null)
            incorrectText.gameObject.SetActive(false);
    }

    /// Call this when the user pressed "Check" and the passcode is WRONG.
    public void PlayIncorrectFeedback()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(IncorrectRoutine());
    }

    private IEnumerator IncorrectRoutine()
    {
        // Show text
        if (incorrectText != null)
            incorrectText.gameObject.SetActive(true);

        // Flash red twice
        if (flashTarget != null)
        {
            for (int i = 0; i < flashCount; i++)
            {
                flashTarget.color = Color.red;
                yield return new WaitForSeconds(flashOnTime);

                flashTarget.color = originalFlashColor;
                yield return new WaitForSeconds(flashOffTime);
            }
        }

        // Keep "INCORRECT" visible briefly, then hide
        yield return new WaitForSeconds(incorrectShowTime);

        if (incorrectText != null)
            incorrectText.gameObject.SetActive(false);

        // Ensure color restored
        if (flashTarget != null)
            flashTarget.color = originalFlashColor;

        routine = null;
    }
}