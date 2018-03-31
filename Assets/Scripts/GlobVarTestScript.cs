using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobVarTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GlobalVariables.xCharacterPosition = 5;
        Debug.Log(GlobalVariables.xCharacterPosition.ToString());

        GlobalVariables.xCharacterPosition = 10;
        Debug.Log(GlobalVariables.xCharacterPosition.ToString());

        GlobalVariables.xCharacterPosition = 15;
        Debug.Log(GlobalVariables.xCharacterPosition.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
