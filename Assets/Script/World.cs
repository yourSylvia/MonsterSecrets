using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;

using System.IO;

public class World : MonoBehaviour {
	private bool isGenerateMonsters=true;
	private int genTimes = 1;
	private int genSec = 3;

	private class Sign{
		public string signStr{set;get;}
		public GameObject signGameObject{ get; set;}
	}
	// Use this for initialization
	private class MonsterObject {
		public MonsterObject(GameObject gameObj,string signStr,ArrayList dotList,GameObject signObject){
			this.gameObj=gameObj;
			this.signStr=signStr;
			this.dotList = dotList;
			this.signObject=signObject;
 		}
		public GameObject gameObj {
			set;
			get;
		}
		public string signStr { set; get;}
		public ArrayList dotList {set;get;}
		public GameObject signObject {set;get;}
		public bool isBoss{set;get;}

	}
//	Dictionary<string,List<MonsterObject>> monsterTable = new Dictionary<string,List<MonsterObject>>();
	List<MonsterObject> monsterList  = new List<MonsterObject>();

	private string[] signs= {"S","3-transpose","W","mountain","vw" };
	public Transform referenceRoot;


//	GesturePatternDraw[] references;

	void Start () {
//		InvokeRepeating ("Gen", 2, 4);
		Invoke("Gen",5);


//		references = referenceRoot.GetComponentsInChildren<GesturePatternDraw> ();

//		SignObject signObj = new SignObject (GameObject.Find ("M1_Wraper"), "s");
//		monsterList.Add (signObj);
//		GenBoss();
	 
	}

	void GenBoss(){
 		GameObject prototype = Resources.Load("Boss") as GameObject;
		prototype = Instantiate(prototype);

		float[,] xys={{-3.41f,0.797f},{-2.144f,2.117f},{0,2.63f},{2.32f,2.117f},{3.63f,0.817f}};

		for (int i = 0; i < 5; i++) {
			ArrayList dotsList = new ArrayList ();
		
			Sign sign = CreateSign (xys[i,0],xys[i,1],prototype.transform.GetChild (0));
			for (int j = 0; j < 3; j++){
				dotsList.Add (CreateDot(j,sign.signGameObject.transform));
			}
			MonsterObject monsterObj = new MonsterObject (prototype, sign.signStr,dotsList,sign.signGameObject){isBoss=true};
			monsterList.Add(monsterObj);
		}
		prototype.transform.position = new Vector2(-7f,0f);  

	}

	Sign CreateSign(float x, float y,Transform parent){
		string signStr = signs [Random.Range (0, signs.Length )];
		GameObject signGameObject = Resources.Load ("Sign/" + signStr) as GameObject;
		signGameObject = Instantiate(signGameObject);
		signGameObject.transform.parent = parent;
		signGameObject.transform.localPosition=new Vector2 (x, y);
		signGameObject.AddComponent<Animation> ();
		AnimationClip clips = Resources.Load("Anim/blink") as AnimationClip;
		signGameObject.GetComponent<Animation> ().AddClip (clips, "blink");
		signGameObject.GetComponent<Animation> ().Blend ("blink");
//		signGameObject.GetComponent<Animation> ().Play ();
		Sign sign=new Sign(){signStr=signStr,signGameObject=signGameObject};
		return sign;
	}

	GameObject CreateDot(int dotPosition,Transform parent){
		GameObject dot = Resources.Load("Sign/dot") as GameObject;
		dot = Instantiate(dot);
		dot.transform.parent =parent ;
		dot.transform.localPosition=new Vector2 (-0.3f+0.3f*dotPosition, -0.55f);
		dot.transform.localScale = new Vector2(0.2f,0.2f);
		return dot;
	}
		
	void Gen(){
		if(genTimes%7 == 0){
			GenBoss ();
			isGenerateMonsters = false;
		}
		if (isGenerateMonsters) {
			Invoke ("Gen",genSec-Mathf.Pow(0.5f,genTimes));

		}


		GameObject prototype = Resources.Load("M"+Random.Range(1,8)) as GameObject;
		prototype = Instantiate(prototype);

		Sign sign = CreateSign (0, 3f, prototype.transform.GetChild (0));
//		sign.GetComponent<Animation> ().playAutomatically = true;

//		sign.

		int dotsLength = Random.Range (0, 3);
		ArrayList dotsList = new ArrayList ();
		for (int i = 0; i < dotsLength; i++) {
			GameObject dot = CreateDot (i, sign.signGameObject.transform);
			dotsList.Add (dot);
		}

		MonsterObject monsterObj = new MonsterObject (prototype, sign.signStr,dotsList,sign.signGameObject);
 		


		
		prototype.transform.position = new Vector2(-10f,0f);  
		monsterList.Add(monsterObj);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnGestureRecognition(Result r) {
		Debug.Log("Gesture is " + r.Name + " and scored: " + r.Score);
	}


	void OnEnable() {
		GestureBehaviour.OnRecognition += OnGestureRecognition;
	}
	void OnDisable() {
		GestureBehaviour.OnRecognition -= OnGestureRecognition;
	}

	void OnDestroy() {
		GestureBehaviour.OnRecognition -= OnGestureRecognition;
	}

	public void OnRecognize(RecognitionResult result){
		StopAllCoroutines ();

		GameObject.Find ("Line").GetComponent<CanvasRenderer> ().Clear ();	
		if (result != RecognitionResult.Empty) {
			Debug.Log ("result:" + result.gesture.id);

		}
		if (result != RecognitionResult.Empty) {
			string name = result.gesture.id;

			for(int i =monsterList.Count-1;i>=0;i--){
				Debug.Log ((monsterList [i]).signStr);

				if ((monsterList [i]).signStr == name) {

					GameObject gameObj = (monsterList[i] as MonsterObject).gameObj;


					GameObject.Find ("score_txt").GetComponent<Text> ().text =  (int.Parse (GameObject.Find ("score_txt").GetComponent<Text> ().text) + 100).ToString();
					int dotLength = monsterList [i].dotList.Count;

					if (dotLength == 0) {
						Destroy (monsterList [i].signObject);

						if ((monsterList [i] as MonsterObject).isBoss) {
							(monsterList [i] as MonsterObject).gameObj.GetComponent<Boss>().bossLives--;
							if ((monsterList [i] as MonsterObject).gameObj.GetComponent<Boss> ().bossLives > 0) {
								monsterList.Remove (monsterList [i]);
								continue;
							} else {
								isGenerateMonsters = true;
								Invoke ("Gen",0);
							}
						}
						monsterList.Remove (monsterList [i]);

						Animator anim = gameObj.GetComponentsInChildren<Animator> ()[0];

						gameObj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
						gameObj.GetComponent<Rigidbody2D> ().isKinematic = true;

						anim.Play ("MonsterDie");			
						genTimes++;


					} else {

						Sign sign = CreateSign (0, 3, gameObj.transform.GetChild (0));
						sign.signGameObject.transform.position=monsterList [i].signObject.transform.position;
						monsterList [i].signStr = sign.signStr;

						Destroy (monsterList [i].signObject);
						Destroy( (monsterList [i]).dotList[dotLength-1]as GameObject);

						monsterList [i].signObject = sign.signGameObject;
						monsterList [i].dotList.RemoveAt(dotLength-1);

						for (int j = 0; j < monsterList [i].dotList.Count; j++) {
							(monsterList [i].dotList [j] as GameObject).transform.parent = sign.signGameObject.transform;
						}


					}

				}

			}
 		} else {
			Debug.Log ("not recognized");
		}
	}
}
