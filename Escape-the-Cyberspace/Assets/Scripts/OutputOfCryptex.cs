using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutputOfCryptex : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private Image flashTarget;          // background panel OR overlay image
    [SerializeField] private TMP_Text incorrectText;     // "INCORRECT" text
    [SerializeField] private TMP_Text correctText;
    [SerializeField] private GameObject uiHide;

    [Header("Tuning")]
    [SerializeField] private float flashOnTime = 0.12f;
    [SerializeField] private float flashOffTime = 0.10f;
    [SerializeField] private int incorrectFlashCount = 2;
    [SerializeField] private int correctFlashCount = 2;


    [SerializeField] private float messageShowTime = 0.8f;

    private Color originalFlashColor;
    private Coroutine routine;

    private void Awake()
    {
        if (flashTarget != null)
            originalFlashColor = flashTarget.color;

        if (incorrectText != null)
            incorrectText.gameObject.SetActive(false);
        if (correctText != null) 
            correctText.gameObject.SetActive(false);
    }

    /// Call this when the user pressed "Check" and the passcode is WRONG.
    public void PlayIncorrectFeedback()
    {
        StartFeedback(FeedbackType.Incorrect);
    }

    public void PlayCorrectFeedbackAndHideUI()
    {
        StartFeedback(FeedbackType.Correct);
    }

    private void StartFeedback(FeedbackType type)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(FeedbackRoutine(type));
    }

    private IEnumerator FeedbackRoutine(Feedback type)
    {
        // Show text
        if (incorrectText != null)
            incorrectText.gameObject.SetActive(false);
        if (correctText != null)
            correctText.gameObject.SetActive(false);

        Color flashColor = (type == FeedbackType.Correct) ? Color.green : Color.red;
        int flashes = (type == FeedbackType.Correct) ? correctFlashCount : incorrectFlashCount;

        if (type == FeedbackType.Correct)
        {
            if (correctText != null)
                correctText.gameObject.SetActive(false);
        }
        else
        {
            if (incorrectText != null)
                incorrectText.gameObject.SetActive(true);
        }

        if (flashTarget != null)
        {
            for (int i = 0; i < flashes; i++)
            {
                flashTarget.color = flashColor;
                yield return new WaitForSeconds(flashOnTime);

                flashTarget.color = originalFlashColor;
                yield return new WaitForSeconds(flashOffTime);
            }
        }

        yield return new WaitForSeconds(messageShowTime);

        if (incorrectText != null)
            incorrectText.gameObject.SetActive(false);
        if (correctText != null)
            correctText.gameObject.setActive(false);

        if (flashTarget != null)
            flashTarget.color = originalFlashColor;

        // if correct hide UI
        if (type == FeedbackType.Correct && uiHide != null)
            uiHidee.SetActive(false);

        routine = null;
    }
}