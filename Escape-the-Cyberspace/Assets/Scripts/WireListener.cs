using UnityEngine;
using UnityEngine.Events;

public class WireListener : MonoBehaviour
{
    [Header("MAKE SURE: the order of the socket/plugs is the same as in the hierarchy! \nA socket's index must be the same as its plug counterpart!")]
    public WireFemaleSocket[] sockets;
    public WireMalePlug[] plugs;
    public UnityEvent onCorrect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(sockets.Length != plugs.Length)
        {
            Debug.Log("Not enough plugs/sockets!");
        }

        SocketSetup();
        PlugSetup();
    }

    void SocketSetup()
    {
        foreach(WireFemaleSocket socket in sockets)
        {
            socket.acceptedWireID = "id";
        }
    }

    void PlugSetup()
    {
        foreach(WireMalePlug plug in plugs)
        {
            plug.wireID = "id";
        }
    }

    public void CheckCorrectness()
    {
        foreach(WireFemaleSocket socket in sockets)
        {
            if(!socket.isCorrect)
            {
                Debug.Log("One incorrect!");
                return;
            }
        }
        Debug.Log("All correct!");
        onCorrect.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
