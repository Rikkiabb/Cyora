﻿using UnityEngine;
using System.Collections;

public class IcyIntro : MonoBehaviour {
	

	// Use this for initialization
	void Start () {

		StartCoroutine (WaitText ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Icy");
		}


	}
	
	IEnumerator WaitText(){
		
		yield return new WaitForSeconds (11.7f);
		Application.LoadLevel("IcyIntro2");
		
	}
}
