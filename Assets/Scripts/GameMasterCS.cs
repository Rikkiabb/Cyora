using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMasterCS : MonoBehaviour {

	//public static int numbKeys;
	
	public int offsetY = 40;
	public int offsetX = 40;
	public int sizeX = 100;
	public int sizeY = 40;
	public static bool icy = false;
	public static void setIce(bool ice){
		icy = ice;
	}
	
	public static bool isIcy(){
		return icy;
	}
	
	void OnGUI () {
		//GUI.Box(new Rect (offsetX, offsetY, sizeX, sizeY), "Keys\n" + numbKeys );
	}
	
	// This class takes cares of adding and removing the players and enemies
	
	// This function destroys the main player
	// This is a static function because we want to be able to kill from everywhere
	public static void KillPlayer(PlayerScript player){
		Destroy(player.gameObject);
	}
	
	
	public static void KillEnemy(Enemy enemy){
		Destroy(enemy.gameObject);
	}
}
