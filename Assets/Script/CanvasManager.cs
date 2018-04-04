using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour {

	private Animator anim;                          // Reference to the animator component.
	private Canvas canvas;

	private Button[] buttons;


	// Use this for initialization
	void Start () {
		
		canvas = GetComponent<Canvas> ();
		anim = canvas.GetComponent <Animator> ();
		buttons = canvas.GetComponentsInChildren<Button> ();

		Debug.Log ("get canvas");
		Debug.Log ("get" + buttons[0].name);

		buttons [0].onClick.RemoveAllListeners ();
		buttons [0].onClick.AddListener(TaskOnClick);
		Debug.Log("added the listener");

	}

	void TaskOnClick(){

		Debug.Log ("Click menu");

		anim.SetTrigger ("Menu");
		Time.timeScale = 0;

		buttons[1].onClick.AddListener (delegate() {
			Debug.Log("Resume game");
			Destroy(buttons[1]);
			Destroy(buttons[2]);
			Time.timeScale = 1;
		});

		buttons[2].onClick.AddListener (delegate() {
			Debug.Log("End game");
			SceneManager.LoadScene("Map", LoadSceneMode.Single);
		});

	}

}
