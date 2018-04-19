using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCardScript : MonoBehaviour {

    MemoryGameScript memoryGameScript;
	// Use this for initialization
	void Start () {
        memoryGameScript = GetComponentInParent<MemoryGameScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showCard()
    {
        this.gameObject.SetActive(false);

        memoryGameScript.wait(int.Parse(this.gameObject.name.ToString().Substring(4, 2).ToString()));
    }
}
