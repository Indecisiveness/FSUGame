using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadScene(string scene) {
		GameObject myPlayer = GameObject.FindGameObjectWithTag ("Player");

		PlayerScript myScript = myPlayer.GetComponent<PlayerScript> ();

		Scene thisScene = SceneManager.GetActiveScene ();

		myScript.LastScreen = thisScene.name;



        SceneManager.LoadScene(scene);
	}

	public void BackScene(){
		GameObject myPlayer = GameObject.FindGameObjectWithTag ("Player");

		PlayerScript myScript = myPlayer.GetComponent<PlayerScript> ();

		SceneManager.LoadScene (myScript.LastScreen);
	}


    public void quit()
    {
        Application.Quit();
    }
}
