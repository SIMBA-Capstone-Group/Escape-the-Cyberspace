using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrapDoorTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Laboratory";
    [SerializeField] private CanvasGroup fadeCanvas; // assign FadePanel
    [SerializeField] private float fadeDuration = 0.25f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(Transition(other.gameObject));
        }
    }

    IEnumerator Transition(GameObject player)
{
    // 🔒 Stop player movement
    Rigidbody rb = player.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    // 🌑 Fade to black
    float t = 0;
    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        fadeCanvas.alpha = t / fadeDuration;
        yield return null;
    }

    // 🚀 Start loading scene in background
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

    // ⏳ Wait until loading finishes
    while (!operation.isDone)
    {
        yield return null;
    }
}
}