//Class for the scene changer, which persists across scenes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public string lastScene;
    

    SceneChanger instance = null;

    // Use this for initialization
    private void Start()
    {
        lastScene = "nothing";
    }

    void Awake()
    {

        if (instance != null)//disallow duplicates
        {

            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void loadScene(string scene) {
		//GameObject myPlayer = GameObject.FindGameObjectWithTag ("Player");

		//PlayerScript myScript = myPlayer.GetComponent<PlayerScript> ();

		lastScene = SceneManager.GetActiveScene().name;
        Debug.Log(lastScene);
        
        //myScript.LastScreen = thisScene.name;
        SceneManager.LoadScene(scene);
	}

	public void BackScene(){
		//GameObject myPlayer = GameObject.FindGameObjectWithTag ("Player");

		//PlayerScript myScript = myPlayer.GetComponent<PlayerScript> ();

        SceneManager.LoadScene(lastScene);
	}


    public void quit()
    {
        Application.Quit();
    }
}
