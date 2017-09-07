using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseInput : MonoBehaviour {


	// also Input.GetButtonDown("SubmitPremature") for entering morse quickly

	public List<string> ariseAlphabet;
	public Sprite dotImg;
	public Sprite dashImg;

	private float commandEndTimer;
	private bool commandBeingEntered = false;

	private float buttonHeldTime;

	private string result = "";

	private GameObject needleGameObject;
	private Vector2 needleStartPosition;
	private Pool dotPool;

	private AudioSource morseAudioSource;

	private void Start() {
		morseAudioSource = GetComponent<AudioSource>();


		needleGameObject = transform.Find("Needle").gameObject;
		needleStartPosition = needleGameObject.GetComponent<RectTransform>().anchoredPosition;
		dotPool = GameObject.Find("MorseChar Pool").GetComponent<Pool>();

		for (int i = 0; i < dotPool.pooledObjects.Count; i++) {

			dotPool.pooledObjects[i].SetActive(true);
		}
	}
	private void EmptyVisualField() {

		needleGameObject.GetComponent<RectTransform>().anchoredPosition = needleStartPosition;

		for (int i = 0; i < dotPool.pooledObjects.Count; i++) {

			dotPool.pooledObjects[i].GetComponent<Image>().enabled = false;
		}
		//dotPool.DeactivateAllMembers();
		//dashPool.DeactivateAllMembers();
	}
	private void CompareInputToAlphabet(string input) {

		EmptyVisualField();

		for (int i = 0; i < ariseAlphabet.Count; i += 1) {
			if (ariseAlphabet[i] == result) {

				if (i == 0) {

					print("Zero");
				}
				else if (i == 1) {

					print("One");
				}
				else if (i == 2) {

					print("Two");
				}
			}
		}

		print(result);
	}
	private void EnterCharacter() {
		commandBeingEntered = true;

		string characterEntered;

		if (buttonHeldTime < 0.2f) {
			characterEntered = ".";
			result = result + ".";
		}
		else {
			characterEntered = "-";
			result = result + "-";
		}

		if (commandBeingEntered == true && commandEndTimer > 0.5f) {
			commandBeingEntered = false;
			CompareInputToAlphabet(result);
		}

		buttonHeldTime = 0;
		// reset command timer
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
		
		
		morseAudioSource.Stop();
	}
	private void Update () {

		if (Input.GetButton("Action") == true) {
			commandBeingEntered = true;

			buttonHeldTime += Time.deltaTime;
			// reset command timer
			commandEndTimer = 0;

			needleGameObject.GetComponent<FlashEntity>().ForceTrue();
		}
		if (Input.GetButton("Action") == false) {

			commandEndTimer += Time.deltaTime;
		}
		
		if (Input.GetButtonDown("Action") == true) {
			commandBeingEntered = false;
			morseAudioSource.Play();
		}

		if (Input.GetButtonUp("Action") == true) {
			EnterCharacter();
			needleGameObject.GetComponent<FlashEntity>().StartTimer();
		}
		if (Input.GetButtonDown("MorseSubmit") == true) {
			if (Input.GetButton("Action") == true) {
				EnterCharacter();
			}

			commandBeingEntered = false;
			CompareInputToAlphabet(result);
			result = "";
		}

		//if (commandBeingEntered == true) {
		//	commandEndTimer += Time.deltaTime;
		//}
	}
}
