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
		
			if(!windLeft && placement == 0){
				windLeft = true;
				StartCoroutine (waitEffectFirstCloud ());
				Physics2D.gravity = new Vector2(-115, -30);
			}

			if(!windRight && placement == 1){
				windRight = true;
				StartCoroutine (waitEffectSecondCloud ());
				Physics2D.gravity = new Vector2(115, -30);
			}

			if(!windOff && placement == 2){
				windRight = true;
				StartCoroutine (waitEffect ());
				Physics2D.gravity = new Vector2(0, -30);
				GameObject[] arr = GameObject.FindGameObjectsWithTag("Door");
				for(int i = 0; i < arr.Length; i++){
					Destroy (arr[i]);
				}
			}
		
		}
	}

	IEnumerator waitEffect(){

		yield return new WaitForSeconds (1);
		placement++;

	}

	IEnumerator waitEffectFirstCloud(){
		
		yield return new WaitForSeconds (1);
		GameObject[] arr0 = GameObject.FindGameObjectsWithTag("WindCloud");
		for(int i = 0; i < arr0.Length; i++){
			arr0[i].AddComponent<Rigidbody2D> ();
		}
		GameObject fly3 = GameObject.FindGameObjectWithTag("MoveWindCloud");
		Rigidbody2D rb = fly3.GetComponent<Rigidbody2D> ();
		rb.isKinematic = false;
		rb.mass = 6;
		rb.gravityScale = 25;
		placement++;
		GameObject[] arr = GameObject.FindGameObjectsWithTag("Stair");
		for(int i = 0; i < arr.Length; i++){
			arr[i].AddComponent<Rigidbody2D> ();
			Rigidbody2D rb1 = arr[i].GetComponent<Rigidbody2D> ();
			rb1.mass = 0;
			rb1.gravityScale = 15;
		}
		
	}

	IEnumerator waitEffectSecondCloud(){
		
		yield return new WaitForSeconds (1);
		GameObject fly = GameObject.FindGameObjectWithTag("SecondMoveWind");
		Rigidbody2D rb = fly.GetComponent<Rigidbody2D> ();
		rb.isKinematic = false;
		rb.mass = 6;
		rb.gravityScale = 25;
		GameObject fly1 = GameObject.FindGameObjectWithTag("SecondWind");
		fly1.AddComponent<Rigidbody2D> ();
//		Rigidbody2D rb = fly1.GetComponent<Rigidbody2D> ();
//		rb.mass = 3;
//		rb.gravityScale = 50;
		placement++;
		
	}
}
