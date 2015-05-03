#pragma strict

// camera follows player on x-axis so target is player
var realTarget : Transform;
static var target : Transform;

function Start(){
	target = realTarget;
}

function Update () {
	if(target != null){
		// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
		transform.position = target.position + Vector3(0, -target.position.y, -10);
		realTarget = target;
	}
}
