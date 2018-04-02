using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour {

	private float time;
	public Text timeSliderDescText;
	public Text timeSliderLowText;
	public Text timeSliderHighText;
	public Text timeSliderValText;
	public Slider timeSlider;
	public float availableTime;

	void Start () {

	}

	void Update () {
		timeSliderValText.text = timeSlider.value.ToString ("0.##\\%");
		timeSlider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	void ValueChangeCheck(){
		if (timeSlider.value > availableTime) {
			timeSlider.value = availableTime;
		}
		timeSliderValText.text = timeSlider.value.ToString ("0.##\\%");
	}
}
