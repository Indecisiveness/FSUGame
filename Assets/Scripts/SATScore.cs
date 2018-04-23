using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SATScore : MonoBehaviour {

    public int SAT;
    public Text satText;

	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setSAT()
    {
        SAT = Random.Range(1300, 1600);

        getSAT();
    }

    public void getSAT()
    {
        satText.text = "" + SAT;
    }
}
