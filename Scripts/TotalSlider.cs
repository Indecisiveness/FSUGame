using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalSlider : MonoBehaviour {


	public Slider study;
	public Slider work;
	public Slider social;
	public Slider total;

    GameObject player;
    PlayerScript playerScript;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void CheckSliderStatus (){

		total.value = study.value + work.value + social.value;

		if (total.value >= 100) {
			study.interactable = false;
			work.interactable = false;
			social.interactable = false;
		}

	}

   

}
