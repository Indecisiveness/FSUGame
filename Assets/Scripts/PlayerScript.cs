//Class for player information

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour {


    public Sprite[] sprites;

    public string charName;                                 //char name
	public List<int> charStats = new List<int>(7);          //empty list
  //List<string> statNames = new List<string>{"study", "work", "social", "health", "sanity", "motivation","finance"};  //stat names

    public Transcript myTrans;
	public string myMajor;

    public int[] saveMarkers = new int[43 * 3];             // array representing which markers should be active when reloading the Gameboard, 0th index is skipped so naming is 1 to 1, Day1 is index 1, Day2 is index 2 etc...
    public int saveDayBox = 0;
    public int saveMonth = 1;


	//Weekly time allotment
	public float timeStudy = 0;
	public float timeSocial = 0;
	public float timeWork = 0;


	



    static PlayerScript instance = null;

	// Use this for initialization
	void Awake () {

        if (instance != null) {
			//doesn't allow duplicates
            Destroy(gameObject);
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(this);
        }

        resetSaveMarkers();  
        
    }
	
	// Update is called once per frame
	void Update () {

        
	}



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.otherCollider.gameObject.name);
    }

    public void resetSaveMarkers() { for (int i = 1; i < 43 * 3; i++) { saveMarkers[i] = 1; } }

    

    
}
