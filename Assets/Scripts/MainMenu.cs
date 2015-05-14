using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void Quit(){
		//Does nothing in unity but works when running on a operating system
		Debug.Log("You just hit quit");
		Application.Quit();
	}
	
	public void NewGame(){
		Application.LoadLevel("Intro");
	}
	
	public void Instructions(){
		Application.LoadLevel("Instructions");
	}
	
	public void MainMenus(){
		Application.LoadLevel("MainMenu");
	}

	public void Tutor(){
		Application.LoadLevel("TutorLevel");
	}

	public void Level(){
		Application.LoadLevel("SelectLevel");
	}

	public void Sun(){
		Application.LoadLevel("One");
	}
	
	public void Wind(){
		Application.LoadLevel("Two");
	}
	
	public void Ice(){
		Application.LoadLevel("Icy");
	}
	
	public void Rain(){
		Application.LoadLevel("Rainy");
	}
	

}
