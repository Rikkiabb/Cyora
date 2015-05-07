using UnityEngine;
using System.Collections;

public class LevelTwoMaster : MonoBehaviour {

	bool windLeft, windRight, windOff;
	int placement;

	void Start(){
		windLeft = false;
		windRight = false;
		windOff = false;
		placement = 0;
	}


	void OnTriggerEnter2D (Collider2D obj){
		if (obj.name == "Player") {
			Debug.Log(placement);
		
			if(!windLeft && placement == 0){
				windLeft = true;
				StartCoroutine (waitEffect ());
				Physics2D.gravity = new Vector2(-115, -30);
				GameObject explode = GameObject.FindGameObjectWithTag("Explode2");
				Destroy(explode);
			}

			if(!windRight && placement == 1){
				windRight = true;
				StartCoroutine (waitEffect ());
				Physics2D.gravity = new Vector2(115, -30);
				GameObject explode = GameObject.FindGameObjectWithTag("Explode");
				Destroy(explode);
			}

			if(!windOff && placement == 2){
				windRight = true;
				StartCoroutine (waitEffect ());
				Physics2D.gravity = new Vector2(0, -30);
				GameObject explode0 = GameObject.FindGameObjectWithTag("Door1");
				Destroy(explode0);
				GameObject explode1 = GameObject.FindGameObjectWithTag("Door2");
				Destroy(explode1);
				GameObject explode2 = GameObject.FindGameObjectWithTag("Door3");
				Destroy(explode2);
				GameObject explode3 = GameObject.FindGameObjectWithTag("Door4");
				Destroy(explode3);
				GameObject explode4 = GameObject.FindGameObjectWithTag("Door5");
				Destroy(explode4);
			}
		
		}
	}

	IEnumerator waitEffect(){

		yield return new WaitForSeconds (10);
		placement++;

	}
}
