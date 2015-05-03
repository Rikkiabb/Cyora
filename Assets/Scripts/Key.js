#pragma strict

var keyEffect : Transform;

function OnTriggerEnter2D (obj : Collider2D){
	
	if(obj.name == "Player"){
		
		GameMaster.numbKeys++;
		var effect = Instantiate(keyEffect, transform.position, transform.rotation);
		Destroy(effect.gameObject, 2);
		Destroy(gameObject);
	}

}