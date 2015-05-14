using UnityEngine;
using System.Collections;

public class IcyOutro : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine (WaitScene ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Rainy");
		}
		
		
	}
	
	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (10);
		Application.LoadLevel("RainyIntro");
	}

}
