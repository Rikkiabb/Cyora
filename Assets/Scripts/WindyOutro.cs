using UnityEngine;
using System.Collections;

public class WindyOutro : MonoBehaviour {
	
	bool playScene;
	
	// Use this for initialization
	void Start () {
		
		playScene = true;
		StartCoroutine (WaitScene ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Icy");
		}
		
		if (!playScene) {
			Application.LoadLevel("IcyIntro");
		}
	}
	
	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (10);
		playScene = false;
	}
}
