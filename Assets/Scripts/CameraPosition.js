#pragma strict

var target : Transform;

function Update () {
	transform.position = target.position + Vector3(0, -target.position.y, -10);
}