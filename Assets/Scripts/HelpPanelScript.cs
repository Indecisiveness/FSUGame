using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayHelpPanel()
    {
        gameObject.SetActive(true);
    }

    public void HideHelpPanel()
    {
        gameObject.SetActive(false);
    }
}
