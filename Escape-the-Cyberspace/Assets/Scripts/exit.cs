using UnityEngine;

public class Exit : MonoBehaviour
{
    public void DoExitGame()
    {
        Debug.Log("Quit pressed");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
