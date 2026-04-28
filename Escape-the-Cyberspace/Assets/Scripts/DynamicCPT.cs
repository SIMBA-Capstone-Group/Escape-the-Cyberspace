using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DynamicCPT : MonoBehaviour
{
    public TextMeshProUGUI[] employeeSubnet;
    public TextMeshProUGUI[] adminSubnet;
    public TextMeshProUGUI[] utilitiesSubnet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string [] mask1 = GetRandomIP(employeeSubnet.Length, "10.0.3.");

        for(int i = 0; i > employeeSubnet.Length; i++)
        {
            employeeSubnet[i].text = mask1[i];
        }

        string[] mask2 = GetRandomIP(adminSubnet.Length, "10.0.1.");

        for(int i = 0; i > adminSubnet.Length; i++)
        {
            adminSubnet[i].text = mask2[i];
        }

        string[] mask3 = GetRandomIP(utilitiesSubnet.Length, "10.0.2.");

        for(int i = 0; i > utilitiesSubnet.Length; i++)
        {
            utilitiesSubnet[i].text = mask3[i];
        }
    }

    private string[] GetRandomIP(int length, string header)
    {
        string[] ips = {};
        int[] chosenMasks = {};
        for(int i = 0; i < length; i++)
        {
            int temp = 0;
            do
            {
                temp = Random.Range(2, 224);
            }
            while (!chosenMasks.Contains(temp));
            ips.Append(header + (temp).ToString());
        }
        return ips;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
