using UnityEngine;
using System.Collections;

public class GameMasterCS : MonoBehaviour {

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
