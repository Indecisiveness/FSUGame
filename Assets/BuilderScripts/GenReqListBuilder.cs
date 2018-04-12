using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenReqListBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GenReqList MyGenReqList = (GenReqList) ScriptableObject.CreateInstance("GenReqList");

		List<Object> AllReqs = new List<Object> ();

		AllReqs.AddRange(Resources.LoadAll ("GenReq", typeof(GenReq)));

		AllReqs.ForEach (x => MyGenReqList.MyGenReqs.Add ((GenReq)x));

		AssetDatabase.CreateAsset (MyGenReqList, "Assets/Resources/MyGenReqList.asset");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
