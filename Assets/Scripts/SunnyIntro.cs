using UnityEngine;
using System.Collections;

public class SunnyIntro : MonoBehaviour {

	
	// Use this for initialization
	void Start () {

		StartCoroutine (WaitCloud ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("One");
		}

	}
	
	IEnumerator WaitCloud(){
		
		yield return new WaitForSeconds (7);
		Destroy (GameObject.Find ("CloudFull").gameObject);
		StartCoroutine (WaitScene ());
	}

	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (2);
		Application.LoadLevel("One");
	}
}
