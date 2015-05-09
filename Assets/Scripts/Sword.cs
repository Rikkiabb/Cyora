using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	PlayerScript player;

	void Start(){
		player = transform.parent.gameObject.GetComponent<PlayerScript> ();
	}

	void OnTriggerEnter2D(Collider2D obj){

//		anim.SetTrigger("Swing");


		if (obj.gameObject.tag == "Enemy" && !player.allowAttack) {
			Debug.Log("WE HIT!");
			Enemy enemy = obj.gameObject.GetComponent<Enemy>();
			enemy.DamageEnemy(1);
			//Destroy(obj.transform.parent.gameObject);
//			Destroy(obj.gameObject.pa);
		}
	}

	void OnCollisionEnter2D(Collision2D obj){
		
		//		anim.SetTrigger("Swing");
		
		
		if (obj.gameObject.tag == "Enemy" && !player.allowAttack) {
			Debug.Log("WE HIT!");
			Enemy enemy = obj.gameObject.GetComponent<Enemy>();
			enemy.DamageEnemy(1);
			//Destroy(obj.transform.parent.gameObject);
			//			Destroy(obj.gameObject.pa);
		}
	}



}
