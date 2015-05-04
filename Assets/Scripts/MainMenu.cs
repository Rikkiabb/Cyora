using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void Quit(){
		//Does nothing in unity but works when running on a operating system
		Debug.Log("You just hit quit");
		Application.Quit();
	}
	
	public void NewGame(){
		Application.LoadLevel("First");
	}
	
	public void Instructions(){
		Application.LoadLevel("Instructions");
	}
	
	public void MainMenus(){
		Application.LoadLevel("MainMenu");
	}
}
