using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	public bool insureTheresOnlyOne = false;
	public static bool inDontDestroyOnLoad = false;

	private void Start () {

		if (insureTheresOnlyOne == true && inDontDestroyOnLoad == true && GameObject.Find(name)) {
			Destroy(gameObject);
		}

		if (insureTheresOnlyOne == true) {
			inDontDestroyOnLoad = true;
		}

		DontDestroyOnLoad(gameObject);
	}
	
}
