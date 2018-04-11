using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : ScriptableObject {

	public string charName;
	public List<int> charStats = new List<int> (7);
	public Transcript myTrans;
	public string myMajor;

	public int saveDayBox = 0;
	public int saveMonth = 1;

	public float timeStudy = 0;
	public float timeSocial = 0;
	public float timeWork = 0;








	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
