using UnityEngine;
using System.Collections;

public class SunnyOutro : MonoBehaviour {
	
	bool playScene;
	
	// Use this for initialization
	void Start () {
		
		playScene = true;
		StartCoroutine (WaitScene ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Two");
		}
		
		if (!playScene) {
			Application.LoadLevel("WindyIntro");
		}
	}
	
	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (5.9f);
		playScene = false;
	}
}
