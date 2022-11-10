using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorizeMenuChildren : MonoBehaviour {

	public Color blackColor;
	public Color greyColor;

	// Use this for initialization
	void Start () {

		Transform listOfMorseTransform = GameObject.Find("ListOfMorse").transform;

		int count = 1;

		foreach(Transform child in listOfMorseTransform) {
			if (count % 2 == 0) {
				child.GetComponent<Image>().color = blackColor;
			}
			else {

				child.GetComponent<Image>().color = greyColor;
			}
			count += 1;
		}

	}
	
}
