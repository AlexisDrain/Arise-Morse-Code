using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

	public string thisLocationInCamera = "Bottom";
	public string goToScene = "0x0y";

	private void OnTriggerEnter (Collider other) {
		
		if (other.tag == "Player") {
			
			GameManager.playerExitLocationInCamera = thisLocationInCamera;
			SceneManager.LoadScene(goToScene);
		}
	}
}
