using UnityEngine;

public class HowToListener : MonoBehaviour
{
    public GameObject howToPlayScreen;
    private bool isOn = false;
    void Start()
    {
        howToPlayScreen.SetActive(false);
    }

    public void ToggleScreen()
    {
        isOn ^= true;
        howToPlayScreen.SetActive(isOn);
    }
}
