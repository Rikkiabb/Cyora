using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public Transform keyEffect;
	
	void OnTriggerEnter2D (Collider2D obj){
		
		if(obj.name == "Player"){

			GameMasterCS.numbKeys++;
//			GameMaster.numbKeys++;
			Object effect = Instantiate(keyEffect, transform.position, transform.rotation);
			GameObject effect2 = GameObject.FindGameObjectWithTag("KeyEffect");
			Destroy(effect2, 2);

			Destroy(gameObject);
		}
	}
}
