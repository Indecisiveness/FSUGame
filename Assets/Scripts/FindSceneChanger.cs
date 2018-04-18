//present in every scene to locate the Scene changer function that persists across scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindSceneChanger : MonoBehaviour {


    GameObject sc;
    SceneChanger scScript;
    GameObject player;
    PlayerScript playerScript;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadScene(string s) {

        sc = GameObject.FindGameObjectWithTag("SceneChanger");
        scScript = sc.GetComponent<SceneChanger>();    

        scScript.loadScene(s);
        
        
    }

    public void backToScene()
    {

        sc = GameObject.FindGameObjectWithTag("SceneChanger");
        scScript = sc.GetComponent<SceneChanger>();

        scScript.loadScene(scScript.lastScene);
    }


}
