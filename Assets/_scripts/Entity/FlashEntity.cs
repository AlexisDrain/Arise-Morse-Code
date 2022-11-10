using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEntity : MonoBehaviour {

	//public bool loopFlash = true;
	public bool startCountdownOnAwake = true;
	public float countdownTime = 3;

	private Image myImgComponent;

	private void Start() {
		myImgComponent = GetComponent<Image>();
	}
	private void OnEnable() {

		if (startCountdownOnAwake == true) {
			StartCoroutine(TimeCountdown());
		}
	}

	public void StartTimer() {
		StartCoroutine(TimeCountdown());
	}
	public void ForceTrue() {
		StopAllCoroutines();
		myImgComponent.enabled = true;
	}
	public void ForceFalse() {
		StopAllCoroutines();

		myImgComponent.enabled = false;
	}
	private void ToggleImage() {

		//if (loopFlash == true) {

		myImgComponent.enabled = !myImgComponent.enabled;
		//}

		StartCoroutine(TimeCountdown());
	}

	// Update is called once per frame
	IEnumerator TimeCountdown() {
		yield return new WaitForSeconds(countdownTime);
		ToggleImage();
	}
}
