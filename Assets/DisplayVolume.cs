using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVolume : MonoBehaviour {


	private float rememberVolume = 2;

	private Slider mySlider;

	private void Start () {

		mySlider = transform.parent.GetComponent<Slider>();
		//myTransform = transform;
		
	}


	private float normalizedVolume;

	private void Update () {

		// normalized = (x-min(x))/(max(x)-min(x))
		normalizedVolume = (mySlider.value +80) / (0 + 80);

		if (rememberVolume != normalizedVolume) {
			rememberVolume = normalizedVolume;

			GetComponent<Text>().text = "(" + rememberVolume.ToString() + ")";
		}
	}

}
