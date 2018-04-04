using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;

public class new_HKIsland : MonoBehaviour {
	
	public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (hit != null && hit.collider != null) {
			SceneManager.LoadScene("HKIsland", LoadSceneMode.Single);

			Color temp = sr.color;
			temp.a = 0f;
			sr.color = temp;

			Debug.Log ("I'm hitting! "+hit.collider.name);
		}
	}
}
	
