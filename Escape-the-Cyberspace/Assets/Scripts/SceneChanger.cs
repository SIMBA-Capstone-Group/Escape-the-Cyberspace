using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class TrapDoorTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Laboratory";
    public Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private MonoBehaviour playerMovementScript;
    [SerializeField] private GameObject loadingText;

    private bool triggered = false;

    private void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }

        if (loadingText != null)
            loadingText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(Transition(other.gameObject));
        }
    }

    private IEnumerator Transition(GameObject player)
    {
        // Stop movement script
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Stop Rigidbody movement if present
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Fade to black
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Show loading text after fade
        if (loadingText != null)
            loadingText.SetActive(true);

        // Start async load
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        // Wait until scene is loaded to 90%
        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        // Small pause so the loading screen is visible
        yield return new WaitForSeconds(1f);

        // Enter scene
        operation.allowSceneActivation = true;
    }
}