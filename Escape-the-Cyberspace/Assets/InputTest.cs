using UnityEngine;
using UnityEngine.InputSystem; 

public class InputTest : MonoBehaviour
{
    public InputActionProperty testActionValue;
    public InputActionProperty testActionButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = testActionValue.action.ReadValue<float>();
        Debug.Log("VALUE: " + value);
        bool button = testActionButton.action.IsPressed();
        Debug.Log("BUTTON: " + button);
    }
}
