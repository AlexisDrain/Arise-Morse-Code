using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseInput : MonoBehaviour {
	
	public Sprite dotImg;
	public Sprite dashImg;

	private float commandEndTimer;

	private float buttonHeldTime;

	private string result = "";

	private Transform listOfMorseTransform;
	private GameObject needleGameObject;
	private Vector2 needleStartPosition;
	private FlashEntity needleFlashEntity;
	private Pool dotPool;

	private AudioSource morseAudioSource;
	private float morseAudioSourceDefaultVolume;

	private void Start() {
		morseAudioSource = GetComponent<AudioSource>();
		morseAudioSourceDefaultVolume = morseAudioSource.volume;

		listOfMorseTransform = GameObject.Find("ListOfMorse").transform;
		needleGameObject = transform.Find("Needle").gameObject;
		needleStartPosition = needleGameObject.GetComponent<RectTransform>().anchoredPosition;
		needleFlashEntity = needleGameObject.GetComponent<FlashEntity>();
		dotPool = GameObject.Find("MorseChar Pool").GetComponent<Pool>();

		for (int i = 0; i < dotPool.pooledObjects.Count; i++) {

			dotPool.pooledObjects[i].SetActive(true);
		}
		EmptyVisualField();
	}
	private void EmptyVisualField() {

		needleGameObject.GetComponent<RectTransform>().anchoredPosition = needleStartPosition;

		for (int i = 0; i < dotPool.pooledObjects.Count; i++) {

			dotPool.pooledObjects[i].GetComponent<Image>().enabled = false;
		}
	}
	private void EnterCommand(string command) {
		
		EmptyVisualField();

		DisplayCommandCharacters[] childrenCommands = listOfMorseTransform.GetComponentsInChildren<DisplayCommandCharacters>();
		
		for (int i = 0; i < childrenCommands.Length; i += 1) {
			if (childrenCommands[i].morseInput == command) {
				childrenCommands[i].UseCommand();
			}
		}

		// DeleteCommand();
		result = "";
	}
	private void EnterCharacter() {

		if (result.Length >= 5) {
			return;
		}

		string characterEntered;

		if (buttonHeldTime < 0.2f) {
			characterEntered = ".";
		}
		else {
			characterEntered = "-";
		}
		result = result + characterEntered;

		buttonHeldTime = 0;
		commandEndTimer = 0;

		if (characterEntered == ".") {
			// dot

			//dotPool.Spawn(needleGameObject.GetComponent<RectTransform>().position + new Vector3(8f, -8f, 1f));
			dotPool.pooledObjects[result.Length - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(16, 16);
			dotPool.pooledObjects[result.Length - 1].GetComponent<Image>().sprite = dotImg;
			dotPool.pooledObjects[result.Length - 1].transform.position = needleGameObject.GetComponent<RectTransform>().position + new Vector3(8f, -8f, 1f);
			needleGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(20f, 0f) + needleGameObject.GetComponent<RectTransform>().anchoredPosition;
		}
		else {
			// dash

			//dotPool.Spawn(needleGameObject.GetComponent<RectTransform>().position + new Vector3 (17f, -8f, 1f));
			dotPool.pooledObjects[result.Length - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(32, 16);
			dotPool.pooledObjects[result.Length - 1].GetComponent<Image>().sprite = dashImg;
			dotPool.pooledObjects[result.Length - 1].transform.position = needleGameObject.GetComponent<RectTransform>().position + new Vector3(17f, -8f, 1f);
			needleGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(38f, 0f) + needleGameObject.GetComponent<RectTransform>().anchoredPosition;
		}
		dotPool.pooledObjects[result.Length - 1].GetComponent<Image>().enabled = true;

		StartCoroutine(VolumeFade(morseAudioSource, 0f, 0.1f));
	}

	IEnumerator VolumeFade(AudioSource audioSourceInput, float endVolume, float timeLength) {

		float startVolume = audioSourceInput.volume;
		float startTime = Time.time;

		while (Time.time < startTime + timeLength) {
			audioSourceInput.volume = startVolume + ((endVolume - startVolume) * (Time.time - startTime) / timeLength);
			
			yield return null;
		}

		if (endVolume == 0f) {
			audioSourceInput.Stop();
		}
	}

	private void Update () {

		if (hardInput.GetKey("KeyMorse") == true || hardInput.GetKey("PadMorse")) {

			buttonHeldTime += Time.deltaTime;
			// reset command timer
			commandEndTimer = 0;

			needleFlashEntity.ForceTrue();
		}
		else {

			commandEndTimer += Time.deltaTime;
		}
		
		if (hardInput.GetKeyDown("KeyMorse") == true || hardInput.GetKeyDown("PadMorse")) {
			StopAllCoroutines();
			morseAudioSource.volume = morseAudioSourceDefaultVolume;
			morseAudioSource.Play();
		}

		if (hardInput.GetKeyUp("KeyMorse") == true || hardInput.GetKeyUp("PadMorse")) {
			EnterCharacter();
			needleFlashEntity.StartTimer();
		}

		if (result.Length > 0 && commandEndTimer > 0.4f) {
			EnterCommand(result);
		}
	}
}
