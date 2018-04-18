using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Orignal Author: Mark Roberts
/// Created: 17 March 2018
/// Function: This class creates the sliders used for setting the weekly time.
/// 
/// </summary>
public class TimeSlider : MonoBehaviour {

	private float time;
	public Text timeSliderDescText;
	public Text timeSliderLowText;
	public Text timeSliderHighText;
	public Text timeSliderValText;
	public Slider timeSlider;
	public float availableTime;

	/// <summary>
	/// Upon screen update this method will set the time slider value and
	/// monitor change of value
	/// </summary>
	void Update () {
		timeSliderValText.text = timeSlider.value.ToString ("0.##\\%");
		timeSlider.onValueChanged.AddListener (delegate {
			ValueChangeCheck ();
		});
	}

	/// <summary>
	/// This method limits the value to available time and
	/// sets the format of the time slider
	/// </summary>
	void ValueChangeCheck(){
		if (timeSlider.value > availableTime) {
			timeSlider.value = availableTime;
		}
		timeSliderValText.text = timeSlider.value.ToString ("0.##\\%");
	}
}
