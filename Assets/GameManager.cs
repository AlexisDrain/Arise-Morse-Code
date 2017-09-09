using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameObject gameManagerGameObject;
	public static GameObject interpreter;
	public static List<GameObject> players = new List<GameObject>();
	public GameObject dot;
	public GameObject dash;

	private void Start () {
		gameManagerGameObject = gameObject;

		interpreter = GameObject.Find("Interpreter");
	}
	
}
