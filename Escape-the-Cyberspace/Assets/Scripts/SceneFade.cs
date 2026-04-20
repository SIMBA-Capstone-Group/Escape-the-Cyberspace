using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image[] fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage[0].color;
            c.a = 0f;
            fadeImage[0].color = c;
        }
    }

    public void FadeToScene(string sceneName)
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image is NOT assigned!");
            return;
        }

        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        float time = 0f;
        Color color = fadeImage[0].color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            foreach(Image image in fadeImage)
            {
                image.color = color;
            }
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}