using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Destory(){
		GameObject.Destroy (gameObject);
	}
	public void DestoryIntro(){
		Debug.Log ("destroy");
		GameObject.Destroy (GameObject.Find("Introduction"));	
	}

	public void PauseGame(){
		Time.timeScale = 0;
	}
}
