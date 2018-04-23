using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNetworkManagerScript : MonoBehaviour
{

    NetworkManagerScript nScript;

    // Use this for initialization
    void Start()
    {

        nScript = GameObject.Find("NetworkManager").GetComponent<NetworkManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartUpHost()
    {
        nScript.StartUpHost();
    }
    public void StartUpClient()
    {
        nScript.StartUpClient();
    }

    public void testSyncVars()
    {
        Debug.Log("Calling testSyncVars on FindNetworkManagerScript");
        nScript.testSyncVars();
    }

    public void PlayGame() { nScript.PlayGame(); }
    public void fillIps() { nScript.fillIPs(); }
}