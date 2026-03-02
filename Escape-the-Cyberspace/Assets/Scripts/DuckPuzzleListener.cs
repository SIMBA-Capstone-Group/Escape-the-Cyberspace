using UnityEngine;
using TMPro;

public class CheckButton : MonoBehaviour
{
    [Header("ENTER 5 UPPERCASE LETTERS, NO NUMBERS.")]
    public string CorrectAnswer;
    [Header("ADD COLORS FOR FLASKS AND DUCKS")]
    public Color[] MatchColors;

    [Header("UI STUFF")]
    public TextMeshProUGUI Letter1;
    public TextMeshProUGUI Letter2;
    public TextMeshProUGUI Letter3;
    public TextMeshProUGUI Letter4;
    public TextMeshProUGUI Letter5;
    [SerializeField] private OutputOfCryptex feedbackUI;

    [Header("CRYPTEX STUFF")]
    public GameObject Key;
    public GameObject KeySpawnPoint;
    //public FixedJoint LidJoint;
    public GameObject Cryptex;
    private int HasBeenPressed;

    [Header("FLASK MATERIALS")]
    public Renderer[] FlaskRenderers;

    [Header("STICKY NOTE LETTERS")]
    public TMP_Text Note1;
    public TMP_Text Note2;
    public TMP_Text Note3;
    public TMP_Text Note4;
    public TMP_Text Note5;

    [Header("DUCKY MATERIALS")]
    public Renderer[] DuckRenderers;

    [Header("DUCKY TEXT OBJECTS")]
    public TMP_Text Duck1;
    public TMP_Text Duck2;
    public TMP_Text Duck3;
    public TMP_Text Duck4;
    public TMP_Text Duck5;

    private string[] DuckBinaries = {"000", "001", "010", "011", "100"};
    private int[] DuckOrder;

    private int[] GenerateRandomOrder(int length)
    {
        int[] order = new int[length];
        for (int i = 0; i < length; i++)
            order[i] = i;

        for (int i = length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (order[i], order[j]) = (order[j], order[i]);
        }

        return order;
    }

    private void InitializePuzzle()
    {
        DuckOrder = GenerateRandomOrder(MatchColors.Length);

        TMP_Text[] DuckTexts = { Duck1, Duck2, Duck3, Duck4, Duck5 };
        TMP_Text[] Notes     = { Note1, Note2, Note3, Note4, Note5 };

        for (int duckIndex = 0; duckIndex < MatchColors.Length; duckIndex++)
        {
            int mappedIndex = DuckOrder[duckIndex];

            // DUCK COLOR
            Material[] duckMats = DuckRenderers[duckIndex].materials;
            duckMats[0].color = MatchColors[mappedIndex]; // body
            duckMats[2].color = MatchColors[mappedIndex]; // wings
            DuckRenderers[duckIndex].materials = duckMats;

            // BINARY ON DUCK
            DuckTexts[duckIndex].text = DuckBinaries[mappedIndex];

            // FLASK COLOR
            Material[] flaskMats = FlaskRenderers[duckIndex].materials;
            flaskMats[0].color = MatchColors[mappedIndex];
            FlaskRenderers[duckIndex].materials = flaskMats;

            // STICKY NOTE LETTER
            Notes[duckIndex].text = CorrectAnswer[mappedIndex].ToString();
        }
    }
    void Start()
    {
        if (CorrectAnswer.Length != 5)
        {
            Debug.LogError("ERROR: String CorrectAnswer must be 5 characters!");
        }

        if (MatchColors.Length != 5)
        {
            Debug.LogError("ERROR: Must have 5 colors");
        }

        if (DuckRenderers.Length != 5)
        {
            Debug.LogError("ERROR: Must have 5 duck renders!");
        }

        if (FlaskRenderers.Length != 5)
        {
            Debug.LogError("ERROR: Must have 5 flask renders!");
        }

        // FUTURE WORK: add a 5-letter randomizer here for random cryptex solutions
        
        InitializePuzzle();
    }

    public void CheckIfCorrect()
    {
        char[] letterArray = {Letter1.text[0], Letter2.text[0], Letter3.text[0], Letter4.text[0], Letter5.text[0]};
        
        for (int i = 0; i <= letterArray.Length; i++)
        {
            if(letterArray[i] != CorrectAnswer[i])
            {
                Debug.Log("Incorrect letter entered");

                if (feedbackUI != null)
                    feedbackUI.PlayIncorrectFeedback();

                return;
            }
        }
        Debug.Log("Correct passphrase entered");
        if (feedbackUI != null)
            feedbackUI.PlayCorrectFeedbackAndHideUI();
        Invoke(nameof(OpenCryptex), 0.9f);
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
