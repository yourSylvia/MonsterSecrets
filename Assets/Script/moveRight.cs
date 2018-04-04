using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveRight : MonoBehaviour {
	public int speed;
	public int missing_num = 0;

	Animator anim;                          // Reference to the animator component.
	Button[] buttons;

	void Awake ()
	{
		// Set up the reference.
		anim = GameObject.Find("Canvas").GetComponent <Animator> ();
		buttons = GameObject.Find ("Canvas").GetComponentsInChildren<Button> ();
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x > 10 && gameObject.GetComponent<Rigidbody2D> ().isKinematic!=true) {
			missing_num++;

			gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
			if (int.Parse (GameObject.Find ("miss_txt").GetComponent<Text> ().text) > 4) {
//				GameObject.Find ("miss_txt").GetComponent<Text> ().text = "5";
				anim.SetTrigger ("GameOver");

				buttons [3].onClick.AddListener (OnClick);
				Destroy (GameObject.Find ("drawDetector"));
				Debug.Log ("Game over");
			} else {
				GameObject.Find ("miss_txt").GetComponent<Text> ().text =  (int.Parse (GameObject.Find ("miss_txt").GetComponent<Text> ().text) + 1).ToString();

			}
		}
	}


	void OnClick(){
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}


	void Jump(int scale){
		Rigidbody2D obj = GetComponent<Rigidbody2D> ();

		obj.AddForce (Vector3.up * 250);
		obj.AddForce (Vector3.right * (60 + scale));
//		obj.velocity = new Vector2 (0, obj.velocity.y);
		obj.rotation=0;

	}
		
	void OnCollisionEnter2D(Collision2D collision)
	{	
				
		if (collision.gameObject.tag == "floor")
			Jump (0);
		else if (collision.gameObject.tag == "uphill")
			Jump (60);
//		else if (collision.gameObject.tag == "uphill")
//			Jump (-10);

	}


	void OnBecameVisible()
	{
		
	}
	void OnBecameInvisible()
	{
		
	}

		
}
