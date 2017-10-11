using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour {


	public AudioMixer myAudioMixer;

	public void SetSFX (float newVolume) {
		
		myAudioMixer.SetFloat("SFXVolume", newVolume);
	}
	public void SetMusic(float newVolume) {

		myAudioMixer.SetFloat("MusicVolume", newVolume);
	}
	
}
