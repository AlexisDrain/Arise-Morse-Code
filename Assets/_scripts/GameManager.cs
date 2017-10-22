using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

	public GameObject dot;
	public GameObject dash;

	public static int floorLayerMask;
	public static int wallsLayerMask;
	public static int usableWallLayerMask;

	public static Pool explosionsPool;
	public static Pool grenadesPool;
	public static Pool rocksPool;
	public static Pool bulletPool;

	public static GameObject gameManagerGameObject;
	public static GameObject interpreter;
	public static GameObject player;
	public static Crosshair crosshair;
	public static GameObject morseCommandSelector;
	public static Transform playerTransform;
	public static BoxCollider playerBoxCollider;
	private TrailRenderer playerTrail;

	public static UnityEvent onSceneLoad = new UnityEvent();
	private string previousSceneName;
	private Vector3 playerGroundOffset;

	private void Awake () {
		
		if (gameManagerGameObject != null) {
			return;
		}

		floorLayerMask = LayerMask.NameToLayer("Floor");
		wallsLayerMask = LayerMask.NameToLayer("Walls");
		usableWallLayerMask = LayerMask.NameToLayer("UsableWall");
		
		gameManagerGameObject = gameObject;
		interpreter = GameObject.Find("Interpreter");

		explosionsPool = transform.Find("ExplosionsPool").GetComponent<Pool>();
		grenadesPool = transform.Find("GrenadesPool").GetComponent<Pool>();
		rocksPool = transform.Find("RocksPool").GetComponent<Pool>();
		bulletPool = transform.Find("BulletPool").GetComponent<Pool>();

		morseCommandSelector = GameObject.Find("Canvas/MorseHighlight");
		crosshair = GameObject.Find("Canvas/Crosshair").GetComponent<Crosshair>();
		player = GameObject.Find("Player");
		playerTransform = player.transform;
		playerBoxCollider = player.GetComponent<BoxCollider>();
		playerTrail = player.transform.Find("Trail").GetComponent<TrailRenderer>();

		playerGroundOffset = new Vector3(0f, player.transform.position.y, 0f);

		SceneManager.sceneLoaded += OldLevelNotNeeded;
		SceneManager.sceneUnloaded += PositionPlayerInNewLevel;
	}


	private string exitCamLocation;

	public void LoadNewScene(string levelName, string argExitCamLocation) {

		playerGroundOffset = new Vector3(0f, player.transform.position.y, 0f);
		previousSceneName = SceneManager.GetActiveScene().name;

		SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
		exitCamLocation = argExitCamLocation;
	}

	private void OldLevelNotNeeded(Scene arg0, LoadSceneMode arg1) {
		
		if (previousSceneName != null) {
			SceneManager.UnloadSceneAsync(previousSceneName);

			rocksPool.DeactivateAllMembers();
			grenadesPool.DeactivateAllMembers();
			explosionsPool.DeactivateAllMembers();
			bulletPool.DeactivateAllMembers();
		}
	}
	private void PositionPlayerInNewLevel(Scene arg0) {
		RepositionPlayerInNewScene(exitCamLocation);
	}


	public void RepositionPlayerInNewScene(string previousExitCamLocation) {

		GameObject[] exitsArray = GameObject.FindGameObjectsWithTag("Exit");
		string enteranceLocation = "";
		Vector3 playerOffset = new Vector3(0f, 0f);

		if (previousExitCamLocation == "Up") {
			enteranceLocation = "Down";
			playerOffset = new Vector3(0f, 0f, 2f);
		} else if (previousExitCamLocation == "Down") {
			enteranceLocation = "Up";
			playerOffset = new Vector3(0f, 0f, -2f);
		}
		else if (previousExitCamLocation == "Left") {
			enteranceLocation = "Right";
			playerOffset = new Vector3(-2f, 0f, 0f);
		}
		else if (previousExitCamLocation == "Right") {
			enteranceLocation = "Left";
			playerOffset = new Vector3(2f, 0f, 0f);
		}

		for (int i = 0; i < exitsArray.Length; i += 1) {
			if (exitsArray[i].GetComponent<ExitController>().thisLocationInCamera == enteranceLocation) {

				Vector3 resetPos = new Vector3(exitsArray[i].transform.position.x, 0f, exitsArray[i].transform.position.z) + playerOffset + playerGroundOffset;
				player.transform.position = resetPos;

				playerTrail.Clear();
				return;
			}
		}

	}


}
