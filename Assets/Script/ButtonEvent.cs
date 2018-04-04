using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour {

	private Animator anim;                          // Reference to the animator component.
	private Canvas canvas;
	private Button btn;
	private Button[] buttons;
	private string btn_name;

	// Use this for initialization
	void Start () {
		anim = GameObject.Find("Canvas").GetComponent <Animator> ();

//		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
//
//		buttons = canvas.GetComponentsInChildren<Button> ();

		btn = this.GetComponent<Button> ();
		btn_name = btn.name;
		Debug.Log (btn_name);
		btn.onClick.AddListener (OnClick);

	}

	void OnClick(){
		
		switch(btn_name){

		case "MenuButton":
			Debug.Log ("Click menu");
			anim.SetTrigger ("Menu");
			GameObject.Find ("drawDetector").transform.localPosition = new Vector2 (10000, 0);
			break;

		case "ResumeButton":
			Debug.Log ("Resume Game");
			anim.SetTrigger ("Idle");
			//   Destroy(buttons[2]);
			//   Destroy(buttons[3]);
			Time.timeScale = 1;
			GameObject.Find ("drawDetector").transform.localPosition = new Vector2 (0, 0);

			break;

		case "EndButton":
			Debug.Log ("End Game");
			SceneManager.LoadScene("Map", LoadSceneMode.Single);
			Time.timeScale = 1;
			break;

		case "Button":
			Debug.Log ("Home");
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
			break;
		case "RestartButton":
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
			Time.timeScale = 1;
			break;
		}

		Debug.Log ("Click menu");
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
