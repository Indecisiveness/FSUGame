using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : MonoBehaviour {

    public float seconds;
    Animator animator;

	// Use this for initialization
	void Start () {

        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("animate", false);
        StartCoroutine("wait");
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator wait() {
        
        yield return new WaitForSeconds(seconds);
        animator.SetBool("animate", true);
    }


}
