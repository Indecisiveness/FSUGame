using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalSlider : MonoBehaviour {


	public Slider study;
	public Slider work;
	public Slider social;

	public Slider total;

	// Use this for initialization
	void Start () {
		
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
