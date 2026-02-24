using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutputOfCryptex : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private Image flashTarget;          
    [SerializeField] private TMP_Text incorrectText;     
    [SerializeField] private TMP_Text correctText;       
    [SerializeField] private GameObject uiHide;          // root UI object to hide on correct

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

        if (incorrectText != null) incorrectText.gameObject.SetActive(false);
        if (correctText != null) correctText.gameObject.SetActive(false);
    }

    public void PlayIncorrectFeedback()
    {
        StartRoutine(IncorrectRoutine());
    }

    public void PlayCorrectFeedbackAndHideUI()
    {
        StartRoutine(CorrectRoutine());
    }

    private void StartRoutine(IEnumerator r)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(r);
    }

    private IEnumerator IncorrectRoutine()
    {
        HideTexts();

        if (incorrectText != null)
            incorrectText.gameObject.SetActive(true);

        yield return Flash(Color.red, incorrectFlashCount);

        yield return new WaitForSeconds(messageShowTime);

        HideTexts();
        RestoreFlash();
        routine = null;
    }

    private IEnumerator CorrectRoutine()
    {
        HideTexts();

        if (correctText != null)
            correctText.gameObject.SetActive(true);

        yield return Flash(Color.green, correctFlashCount);

        yield return new WaitForSeconds(messageShowTime);

        HideTexts();
        RestoreFlash();

        if (uiHide != null)
            uiHide.SetActive(false);

        routine = null;
    }

    private IEnumerator Flash(Color c, int count)
    {
        if (flashTarget == null)
            yield break;

        for (int i = 0; i < count; i++)
        {
            flashTarget.color = c;
            yield return new WaitForSeconds(flashOnTime);

            flashTarget.color = originalFlashColor;
            yield return new WaitForSeconds(flashOffTime);
        }
    }

    private void HideTexts()
    {
        if (incorrectText != null) incorrectText.gameObject.SetActive(false);
        if (correctText != null) correctText.gameObject.SetActive(false);
    }

    private void RestoreFlash()
    {
        if (flashTarget != null)
            flashTarget.color = originalFlashColor;
    }
}