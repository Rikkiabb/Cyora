#pragma strict

function Quit(){
	//Does nothing in unity but works when running on a operating system
	Debug.Log("You just hit quit");
	Application.Quit();
}

function NewGame(){
	Application.LoadLevel("First");
}