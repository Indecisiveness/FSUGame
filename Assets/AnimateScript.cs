using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        MoveToNextDay();
	}

    public string scene;

    public void MoveToNextDay()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2 position = this.transform.position;
            position.x -= 41;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(this.transform.position.x == 555.5 && this.transform.position.y == 40)
            {
                SceneManager.LoadScene(scene);
            }
            else if (this.transform.position.x == 801.5f)
            {
                Vector2 position = this.transform.position;
                position.x -= 738;
                position.y -= 75;
                this.transform.position = position;
            }
            else
            {
                Vector2 position = this.transform.position;
                position.x += 41;
                this.transform.position = position;
                Debug.Log(position.x.ToString());
                Debug.Log(position.y.ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2 position = this.transform.position;
            position.y += 75;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2 position = this.transform.position;
            position.y -= 75;
            this.transform.position = position;
        }
    }

    public void GoForward()
    {

        if (this.transform.position.x == 801.5f)
        {
            Vector2 position = this.transform.position;
            position.x -= 738;
            position.y -= 75;
            this.transform.position = position;
        }

        else
        {
            Vector2 position = this.transform.position;
            position.x += 41;
            this.transform.position = position;
        }

    }


}
