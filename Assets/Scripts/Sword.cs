using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	Animator anim;

	void Start(){
		anim = transform.parent.GetComponent<Animator> ();	
	}

	void OnCollisionEnter2D(Collision2D obj){

//		anim.SetTrigger("Swing");

		if (obj.gameObject.tag == "Enemy" && anim.GetBool("isKnight3Attacking")) {
			Debug.Log("WE HIT!");
			Enemy enemy = obj.gameObject.GetComponent<Enemy>();
			enemy.DamageEnemy(1);
			//Destroy(obj.transform.parent.gameObject);
//			Destroy(obj.gameObject.pa);
		}
	}


}
