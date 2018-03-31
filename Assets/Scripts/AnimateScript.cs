using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimateScript : MonoBehaviour
{
    public float startDay;
    public float endX;
    public string scene;
    private float pixelsPerMoveX = 183;
    private float pixelsPerMoveY = 116;

    void Awake()
    {
            DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
        //Places character in proper position in gameboard
        Vector3 position = this.transform.position;
        position.x += (startDay - 1) * pixelsPerMoveX;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextDay();
    }

    public void MoveToNextDay()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //If player has reached end of month, next month is loaded on next move
            if (this.transform.position.x > endX && this.transform.position.y < 59)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }

            //If player has reached Saturday, moves character to Sunday and load WeekSelect scene on next move
            else if (this.transform.position.x > 1180)
            {
                Vector3 position = this.transform.position;
                position.x -= 1098;
                position.y -= pixelsPerMoveY;
                this.transform.position = position;

                //SceneManager.LoadScene("WeekSelect");
            }

            //Move player to next day
            else
            {
                Vector3 position = this.transform.position;
                position.x += pixelsPerMoveX;
                this.transform.position = position;

                Debug.Log(this.transform.position.x.ToString());
                Debug.Log(this.transform.position.y.ToString());
            }
        }
    }

    public void GoForward()
    {
        //If player has reached end of month, next month is loaded on next move
        if (this.transform.position.x > endX && this.transform.position.y < 59)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

        //If player has reached Saturday, moves character to Sunday and load WeekSelect scene on next move
        else if (this.transform.position.x > 1180)
        {
            Vector3 position = this.transform.position;
            position.x -= 1098;
            position.y -= pixelsPerMoveY;
            this.transform.position = position;

            //SceneManager.LoadScene("WeekSelect");
        }

        //Move player to next day
        else
        {
            Vector3 position = this.transform.position;
            position.x += pixelsPerMoveX;
            this.transform.position = position;

            Debug.Log(this.transform.position.x.ToString());
            Debug.Log(this.transform.position.y.ToString());
        }
    }


}
