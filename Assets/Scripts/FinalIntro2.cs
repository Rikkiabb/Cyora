using UnityEngine;
using System.Collections;

public class FinalIntro2 : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine (WaitScene ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Final");
		}
		
		
	}
	
	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (5);
		Application.LoadLevel("FinalIntro3");
	}
	
}

