using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
	
	public int bossLives=5;

	Animator anim;                          // Reference to the animator component.

	// Use this for initialization
	void Start () {
		
		anim = GameObject.Find("Canvas").GetComponent <Animator> ();

		InvokeRepeating ("Jump", 1, 0.1f);
		gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 20);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.transform.position.x > 1010 && gameObject.GetComponent<Rigidbody2D> ().isKinematic!=true) {

			anim.SetTrigger ("GameOver");

			Destroy (GameObject.Find ("drawDetector"));
			Debug.Log ("Game over");

//			gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
//			if (int.Parse (GameObject.Find ("miss_txt").GetComponent<Text> ().text) > 4) {
//				//				GameObject.Find ("miss_txt").GetComponent<Text> ().text = "5";
//				anim.SetTrigger ("GameOver");
//
//				Destroy (GameObject.Find ("drawDetector"));
//				Debug.Log ("Game over");
//			} else {
//				GameObject.Find ("miss_txt").GetComponent<Text> ().text =  (int.Parse (GameObject.Find ("miss_txt").GetComponent<Text> ().text) + 1).ToString();
//
//			}

		}
 	}


	void Jump(){
//		gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.down*50 );
//		gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 40);
		if (gameObject.transform.position.y > 0 ) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.down * 50);
		} else {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 50);
		}


	}
}
