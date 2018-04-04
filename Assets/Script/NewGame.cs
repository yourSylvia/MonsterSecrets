using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Button btn = this.GetComponent<Button> ();
        btn.onClick.AddListener (OnClick);
	}

	void OnClick()
	{
		SceneManager.LoadScene("Map", LoadSceneMode.Single);
		Debug.Log("Enter to Map");
	}
		
}
