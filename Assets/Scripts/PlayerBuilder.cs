using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


public class PlayerBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {

		PlayerObject myPlayer = ScriptableObject.CreateInstance<PlayerObject> ();
		Transcript TheTrans = Resources.Load<Transcript> ("myTranscript");


		myPlayer.charName = "";
		myPlayer.myTrans = TheTrans;




		AssetDatabase.CreateAsset (myPlayer, "Assets/Resources/MyPlayer.asset");




	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
