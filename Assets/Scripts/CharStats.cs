using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharStats : MonoBehaviour{
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

    }

   
	public List<int> StatValues = new List<int> (7);

	public List<string> StatNames = new List<string> {"Job Skills", "Study Habits", "Social Skills", "Physical Health", "Sanity", "Motivation", "Finances"};

    public Text jobText;
    public Text studyText;
    public Text socialText;
    public Text physicalText;
    public Text sanityText;
    public Text motivationText;
    public Text moneyText;

	public PlayerScript Player1;

    //Stats relative to the following: Job Skills, Study Habits, Social Skills, Physical Health, Sanity, Motivation, Finances
    //Methods can be be modified later for polish



    public void setStatsJoe() //Stats method for Joe Character
    {
		StatValues = new List<int>{1,1,1,1,1,1,1};

        displayStats();    

		Player1.charStats = StatValues;
    }

    public void setStatsJock() //Stats method for Jock character
    {
		StatValues = new List<int>{0,0,3,3,0,0,1};

        displayStats();

		Player1.charStats = StatValues;
    }

    public void setStatsNerd()
    {
		StatValues = new List<int>{0,3,0,0,0,3,1};

        displayStats();

		Player1.charStats = StatValues;
    }

    public void displayStats() //Method to display stats on the character select screen
    {
		jobText.text = StatValues[0].ToString();
		studyText.text = StatValues[1].ToString();
		socialText.text = StatValues[2].ToString();
		physicalText.text = StatValues[3].ToString();
		sanityText.text = StatValues[4].ToString();
		motivationText.text = StatValues[5].ToString();
		moneyText.text = StatValues[6].ToString();
    }

    
        
        
        
        
        
    

}
