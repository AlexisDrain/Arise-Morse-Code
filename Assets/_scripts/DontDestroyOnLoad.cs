using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DontDestroyOnLoad : MonoBehaviour {

	public bool insureTheresOnlyOne = false;

	public void Awake () {

		if (insureTheresOnlyOne == true) {

			DontDestroyOnLoad[] similarObjects = FindObjectsOfType<DontDestroyOnLoad>().Where(obj => obj.name == name).ToArray();

			if (similarObjects.Length > 1) {

				for (int i = 1; i < similarObjects.Length; i += 1) {
					Destroy(similarObjects[i].gameObject);
				}
			}
		}

		DontDestroyOnLoad(gameObject);
	}
	
}
