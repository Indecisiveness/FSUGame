using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerScript : NetworkManager {

    GameObject hostField;
    GameObject p2Field;
    GameObject p3Field;
    GameObject p4Field;

    GameObject NetworkedStats1, NetworkedStats2, NetworkedStats3, NetworkedStats4;

	int num;


    // Use this for initialization
    void Start () {

        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
        NetworkServer.Reset();

		num = 0;
    }
	
	// Update is called once per frame
	void Update () {



		
	}

    public void StartUpHost() {
        
        singleton.networkPort = 7777;
        singleton.StartHost();
        
    }

    public void StartUpClient() {

        string ipTarget = GameObject.Find("InputField").transform.GetChild(2).gameObject.GetComponent<Text>().text;
        Debug.Log("IP Target : " + ipTarget);
        singleton.networkAddress = ipTarget;
        singleton.networkPort = 7777;
		NetworkClient myClient = singleton.StartClient();

		GetReady (myClient);


    }

	IEnumerator GetReady(NetworkClient myClient){
		yield return new WaitForSeconds(2.0f);
		ClientScene.Ready (myClient.connection);
	}

    public void fillIPs()
    {
        hostField = GameObject.Find("HostIPField");
        p2Field = GameObject.Find("Player2IPField");
        p3Field = GameObject.Find("Player3IPField");
        p4Field = GameObject.Find("Player4IPField");

        hostField.GetComponent<Text>().text = UnityEngine.Network.player.ipAddress.ToString();        

        int numConnections = NetworkServer.connections.Count;


        if (numConnections == 2)
        {
            p2Field.GetComponent<Text>().text = NetworkServer.connections[1].address;
        }

        else if (numConnections == 3)
        {

            p2Field.GetComponent<Text>().text = NetworkServer.connections[1].address;
            p3Field.GetComponent<Text>().text = NetworkServer.connections[2].address;

        }
        else if (numConnections == 4)
        {
            p2Field.GetComponent<Text>().text = NetworkServer.connections[1].address;
            p3Field.GetComponent<Text>().text = NetworkServer.connections[2].address;
            p4Field.GetComponent<Text>().text = NetworkServer.connections[3].address;
		}

        
    }

    public void testSyncVars() {

        Debug.Log("Calling testSyncVars on NetworkManagerScript");
        gameObject.transform.GetChild(0).gameObject.GetComponent<NetworkedStatsScript>().testSyncVars();        
    }
		

    public void PlayGame() { 

		singleton.ServerChangeScene("charSelect");

	}   

	public void ChangeMade(){
		GameObject allStats= gameObject.transform.Find ("AllStats").gameObject;

		allStats.GetComponent<AllNetworkedStats> ().GetPlayStats ();


		NetworkedStatsScript[] allPlayers= gameObject.GetComponentsInChildren<NetworkedStatsScript>();
		for (int i = 0; i < allPlayers.Length; i++) {
			allPlayers [i].StatusUpdate ();
		}


	}


	public int pNum(){
		num++;
		return num;
	}



}
