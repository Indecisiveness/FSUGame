using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour {


	public string charName;                                 //char name
	public List<int> charStats = new List<int>(7);          //empty list
  //List<string> statNames = new List<string>{"study", "work", "social", "health", "sanity", "motivation","finance"};  //stat names

    public Transcript myTrans;
	public string myMajor;


    public int saveDayBox = 0;
    public int saveMonth = 1;


	public float timeStudy = 0;
	public float timeSocial = 0;
	public float timeWork = 0;


	



    static PlayerScript instance = null;

	// Use this for initialization
	void Awake () {

        if (instance != null) {

            Destroy(gameObject);
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(this);
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        
	}

	//public void StayAlive () {
		//DontDestroyOnLoad (this);
		//myMajor = myTrans.MajorName;
	//}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.otherCollider.gameObject.name);
    }

    

    
}
