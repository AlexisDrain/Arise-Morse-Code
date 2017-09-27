using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplaySceneName : MonoBehaviour {

	private void Start () {
		SceneManager.sceneLoaded += OnNewScene;
	}

	private void OnNewScene(Scene arg0, LoadSceneMode arg1) {
		GetComponent<Text>().text = arg0.name;
	}
	

}
