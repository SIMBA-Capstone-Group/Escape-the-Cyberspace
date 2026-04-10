using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrapDoorTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Laboratory";
    [SerializeField] private CanvasGroup fadeCanvas; // assign FadePanel
    [SerializeField] private float fadeDuration = 1.5f;

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

        // Optional: disable movement script
        MonoBehaviour movement = player.GetComponent<MonoBehaviour>();
        if (movement != null)
            movement.enabled = false;

        // 🌑 Fade to black
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = t / fadeDuration;
            yield return null;
        }

        // ⏳ Small pause (feels like loading)
        yield return new WaitForSeconds(0.5f);

        // 🚀 Load next scene
        SceneManager.LoadScene(sceneToLoad);
    }
}