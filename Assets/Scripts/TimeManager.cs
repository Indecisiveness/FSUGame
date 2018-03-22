using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	
	public TimeSlider socialTimeSlider;
	public TimeSlider studyTimeSlider;
	public TimeSlider totalTimeSlider;
	public TimeSlider workTimeSlider;
	private float defaultTime = 33;
	private float maxTime = 100;
	private float totalTime;

	void Start () {

		socialTimeSlider.timeSliderDescText.text = "Social Time";
		socialTimeSlider.timeSliderLowText.text = "Introvert";
		socialTimeSlider.timeSliderHighText.text = "Social Butterfly";
		socialTimeSlider.timeSlider.value = defaultTime;
		studyTimeSlider.timeSliderDescText.text = "Study Time";
		studyTimeSlider.timeSliderLowText.text = "Mindless";
		studyTimeSlider.timeSliderHighText.text = "Genius";
		studyTimeSlider.timeSlider.value = defaultTime;
		totalTimeSlider.timeSliderDescText.text = "Total Time";
		totalTimeSlider.timeSliderLowText.text = "Low";
		totalTimeSlider.timeSliderHighText.text = "High";
		workTimeSlider.timeSliderDescText.text = "Work Time";
		workTimeSlider.timeSliderLowText.text = "Lazy Bones";
		workTimeSlider.timeSliderHighText.text = "Industrious";
		workTimeSlider.timeSlider.value = defaultTime;
		
	}
	
	void Update () {
		totalTime = socialTimeSlider.timeSlider.value + studyTimeSlider.timeSlider.value + workTimeSlider.timeSlider.value;
		totalTimeSlider.timeSlider.value = totalTime;
		socialTimeSlider.availableTime = maxTime - (studyTimeSlider.timeSlider.value + workTimeSlider.timeSlider.value);
		studyTimeSlider.availableTime = maxTime - (socialTimeSlider.timeSlider.value + workTimeSlider.timeSlider.value);
		workTimeSlider.availableTime = maxTime - (studyTimeSlider.timeSlider.value + socialTimeSlider.timeSlider.value);
		totalTimeSlider.availableTime = maxTime;
	}
}
