#pragma strict

// camera follows player on x-axis so target is player
var target : Transform;


function Update () {
	if(target != null){
		// target.position is the position of the player, we add 0 to the x-axis, nothing to the y-axis and the z-axis is a constant
		transform.position = target.position + Vector3(0, -target.position.y, -10);
	}
}