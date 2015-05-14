using UnityEngine;
using System.Collections;

public class FinalOutro1 : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine (WaitScene ());
		
	}
	
	
	IEnumerator WaitScene(){
		
		yield return new WaitForSeconds (16.5f);
		Application.LoadLevel("MainMenu");
	}
}
