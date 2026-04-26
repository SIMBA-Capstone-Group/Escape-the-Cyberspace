using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    public GameObject[] levelScreens;
    private int currentIndex = 0;
    private bool isOn = false;

    void Start()
    {
        TurnOffScreens();
    }

    public void ToggleLevelScreens()
    {
        isOn ^= true;
        if(isOn)
        {
            UpdateScreens();
        }
        else
        {
            TurnOffScreens();
        }
    }

    public void GoRight()
    {
        currentIndex = Mathf.Min(currentIndex + 1, levelScreens.Length - 1);
        UpdateScreens();
    }

    public void GoLeft()
    {
        currentIndex = Mathf.Max(currentIndex - 1, 0);
        UpdateScreens();
    }

    void UpdateScreens()
    {
        for (int i = 0; i < levelScreens.Length; i++)
        {
            levelScreens[i].SetActive(i == currentIndex);
        }
    }

    void TurnOffScreens()
    {
        foreach(GameObject screen in levelScreens)
        {
            screen.SetActive(false);
        }
    }
}