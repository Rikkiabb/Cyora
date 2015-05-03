#pragma strict

//static so we can access it from all scripts
static var numbKeys : int = 0;
var test = 0;

var offsetY : int = 40;
var offsetX : int = 40;
var sizeX : int = 100;
var sizeY : int = 40;


function OnGUI () {
	GUI.Box(new Rect (offsetX, offsetY, sizeX, sizeY), "Keys\n" + numbKeys );
	
}
