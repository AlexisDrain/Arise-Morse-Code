using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

	public GameObject dot;
	public GameObject dash;
	
	public static int wallsLayerMask;
	public static int usableWallLayerMask;
	
	public static GameObject gameManagerGameObject;
	public static GameObject interpreter;
	public static List<GameObject> players = new List<GameObject>();

	public static UnityEvent onNewTurn = new UnityEvent();
	private float newTurnTimerDefault = 3f;
	private float newTurnTimer = 3f;


	private void Awake () {

		wallsLayerMask = LayerMask.NameToLayer("Walls");
		usableWallLayerMask = LayerMask.NameToLayer("UsableWall");

		gameManagerGameObject = gameObject;
		interpreter = GameObject.Find("Interpreter");
		
		GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");
		
		for (int i = 0; i < playersArray.Length; i += 1) {
			if (playersArray[i]) {
				players.Add(playersArray[i]);
			}
		}
	}
	public void Update() {
		newTurnTimer -= Time.deltaTime;

		if (newTurnTimer <= 0) {
			newTurnTimer = newTurnTimerDefault;
			onNewTurn.Invoke();
		}
	}
	public void LoadNewScene(string levelName, string exitCamLocation) {
		
		SceneManager.LoadScene(levelName);
		RepositionPlayerInNewScene(exitCamLocation);
	}
	public void RepositionPlayerInNewScene(string previousExitCamLocation) {

		GameObject[] exitsArray = GameObject.FindGameObjectsWithTag("Exit");
		string enteranceLocation = "";

		Vector3 playerOffset = new Vector3(0f, 0f);
		if (previousExitCamLocation == "Top") {
			enteranceLocation = "Bottom";
			playerOffset = new Vector3(0f, 2f);
		} else if (previousExitCamLocation == "Bottom") {
			enteranceLocation = "Top";
			playerOffset = new Vector3(0f, -2f);
		}
		else if (previousExitCamLocation == "Left") {
			enteranceLocation = "Right";
			playerOffset = new Vector3(-2f, 0f);
		}
		else if (previousExitCamLocation == "Right") {
			enteranceLocation = "Left";
			playerOffset = new Vector3(2f, 0f);
		}

		for (int i = 0; i < exitsArray.Length; i += 1) {
			if (exitsArray[i].GetComponent<ExitController>().thisLocationInCamera == enteranceLocation) {

				Vector3 resetPos = new Vector3(exitsArray[i].transform.position.x, exitsArray[i].transform.position.y, 0f) + playerOffset;
				GameManager.players[0].transform.position = resetPos;
				//GameManager.players[0].GetComponent<PlayerMovement>().PositionPlayer(exitsArray[i].transform.position + playerOffset);
				return;
			}
		}

		players[0].transform.Find("Trail").GetComponent<TrailRenderer>().Clear();
	}


}
