using UnityEngine;
using System.Collections;

public class IcyIntro : MonoBehaviour {

	
	// Use this for initialization
	void Start () {

		StartCoroutine (WaitFirstText ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Fire1")) {
			Application.LoadLevel("Icy");
		}
		

	}
	
	IEnumerator WaitFirstText(){
		
		yield return new WaitForSeconds (5);
		GameObject.Find ("Text").gameObject.SetActive (false);
		GameObject.Find ("TextWrapper").transform.Find ("Text 1").gameObject.SetActive(true);
		StartCoroutine (WaitSecondText ());
	}

	IEnumerator WaitSecondText(){
		
		yield return new WaitForSeconds (7);
		Application.LoadLevel("Icy");
	}
}
