using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class LoadSceneOnSnap : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    public string sceneName = "NextScene";
    public string requiredTag = "Snappable";

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnObjectSnapped);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjectSnapped);
    }

    private void OnObjectSnapped(SelectEnterEventArgs args)
    {
        GameObject snappedObject = args.interactableObject.transform.gameObject;

        if (snappedObject.CompareTag(requiredTag))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}