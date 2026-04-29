using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicCPT : MonoBehaviour
{
    public TextMeshProUGUI[] employeeSubnet;
    public TextMeshProUGUI[] adminSubnet;
    public TextMeshProUGUI[] utilitiesSubnet;

    public TextMeshProUGUI[] stickyNotes;

    void Start()
    {
        string[] mask1 = GetRandomIP(employeeSubnet.Length, "10.0.3.");
        for (int i = 0; i < employeeSubnet.Length; i++)
        {
            employeeSubnet[i].text = mask1[i];
            
        }

        string[] mask2 = GetRandomIP(adminSubnet.Length, "10.0.1.");
        for (int i = 0; i < adminSubnet.Length; i++)
        {
            adminSubnet[i].text = mask2[i];
        }

        string[] mask3 = GetRandomIP(utilitiesSubnet.Length, "10.0.2.");
        for (int i = 0; i < utilitiesSubnet.Length; i++)
        {
            utilitiesSubnet[i].text = mask3[i];
            stickyNotes[i].text = mask3[i];
        }
    }

    private string[] GetRandomIP(int length, string header)
    {
        List<string> ips = new List<string>();
        HashSet<int> used = new HashSet<int>();

        while (ips.Count < length)
        {
            int temp = Random.Range(2, 224);

            if (!used.Contains(temp))
            {
                used.Add(temp);
                ips.Add(header + temp.ToString());
            }
        }

        return ips.ToArray();
    }
}