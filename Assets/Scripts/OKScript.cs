using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKScript : MonoBehaviour {

    GameObject card;

	// Use this for initialization
	void Start () {

        card = this.transform.parent.gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
		
		Card mycard = card.GetComponent<Card> ();

		mycard.GenerateEffect ();

        Destroy(card);

    }
}
