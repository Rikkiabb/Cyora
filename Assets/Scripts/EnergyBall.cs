using UnityEngine;
using System.Collections;

public class EnergyBall : MonoBehaviour {

	Animator anim;
	bool canHurt = true;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 8);
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D obj){
		PlayerScript player = obj.gameObject.GetComponent<PlayerScript> ();
		if (obj.collider.tag == "Sword" && player.isAttacking()) {
			anim.SetTrigger("Explode");
			
			Destroy (gameObject, 0.15f);
			return;
		}
		if (obj.gameObject.tag == "Player") {

			if(canHurt){
				player.DamagePlayer(1);
				canHurt = false;
				StartCoroutine(WaitHurt());
			}
//			Debug.Log (player.isHurt);
			anim.SetTrigger("Explode");

			Destroy (gameObject, 0.15f);

		}
	}

	IEnumerator WaitHurt(){
		yield return new WaitForSeconds (2);
		canHurt = true;
	}

}
