using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceFade : MonoBehaviour {

	/*
	 * Doesn't supporting changing default volume after Start()
	 */ 
	private AudioSource myAudioSource;
	private float defaultVolume;

	private float speedMultiplier = 0;

	private void Start () {
		myAudioSource = GetComponent<AudioSource>();
		defaultVolume = myAudioSource.volume;
	}
	private void SetVolumeToValue(float newVolume) {
		myAudioSource.volume = newVolume;
	}


	public void StartFadeIn(float newSpeedMuilt = 0.5f) {

		speedMultiplier = newSpeedMuilt;
		StartCoroutine("FadeIn");
	}

	public void StartFadeOut(float newSpeedMuilt = 0.5f) {

		speedMultiplier = newSpeedMuilt;
		StartCoroutine("FadeOut");
	}

	IEnumerator FadeIn() {
		for (float f = 0f; f < defaultVolume; f += Time.deltaTime * speedMultiplier) {

			myAudioSource.volume = f;
			
			yield return null;
		}
		// after loop: result
		SetVolumeToValue(defaultVolume);
	}
	IEnumerator FadeOut() {

		SetVolumeToValue(defaultVolume);

		for (float f = defaultVolume; f > 0; f -= Time.deltaTime * speedMultiplier) {

			myAudioSource.volume = f;
			yield return null;
		}

		// after loop: result
		SetVolumeToValue(0f);
	}

}
