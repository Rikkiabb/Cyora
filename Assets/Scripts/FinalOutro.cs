﻿using UnityEngine;
using System.Collections;

public class FinalOutro : MonoBehaviour {

	
	// Use this for initialization
	void Start () {

		StartCoroutine (WaitScene ());
		
	}

	
	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (5.8f);
		Application.LoadLevel("FinalOutro1");
	}
}
