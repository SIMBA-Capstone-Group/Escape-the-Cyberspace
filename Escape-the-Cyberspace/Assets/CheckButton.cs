using UnityEngine;
using TMPro;

public class CheckButton : MonoBehaviour
{
    [Header("UI STUFF")]
    public TextMeshProUGUI Letter1;
    public TextMeshProUGUI Letter2;
    public TextMeshProUGUI Letter3;
    public TextMeshProUGUI Letter4;
    public TextMeshProUGUI Letter5;

    [Header("ENTER 5 UPPERCASE LETTERS, NO NUMBERS.")]
    public string CorrectAnswer;

    [Header(" ")]
    [Header("CRYPTEX STUFF")]
    public GameObject Key;
    public GameObject KeySpawnPoint;
    //public FixedJoint LidJoint;
    public GameObject Cryptex;
    private int HasBeenPressed;
    void Start()
    {
        if (CorrectAnswer.Length != 5)
        {
            Debug.Log("ERROR: String CorrectAnswer must be 5 characters!");
        }
    }

    public void CheckIfCorrect()
    {
        char[] letterArray = {Letter1.text[0], Letter2.text[0], Letter3.text[0], Letter4.text[0], Letter5.text[0]};
        
        for (int i = 0; i <= 4; i++)
        {
            if(letterArray[i] != CorrectAnswer[i])
            {
                Debug.Log("Incorrect letter entered");
                return;
            }
        }
        Debug.Log("Correct passphrase entered");
        OpenCryptex();
    }

    private void OpenCryptex()
    {
        if (HasBeenPressed != 1)
        {
            Instantiate(Key, KeySpawnPoint.transform.position, KeySpawnPoint.transform.rotation);
            //Destroy(LidJoint);
            Destroy(Cryptex);
            Debug.Log("Spawned Key");
            HasBeenPressed = 1;
        }
        else
        {
            Debug.Log("Already pressed button...");
        }
    }


}
