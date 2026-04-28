using UnityEngine;

public class AdminLoginManager : MonoBehaviour
{
    public LoginManager loginScreen;

    public GameObject targetScreen;
    public GameObject loadingScreen;

    public bool isLoggedIn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToggleScreens()
    {
        loginScreen.ChangeScreen(0, targetScreen);
        if (isLoggedIn)
        {
            loadingScreen.SetActive(false);
            targetScreen.SetActive(true);
        }
    }
}
