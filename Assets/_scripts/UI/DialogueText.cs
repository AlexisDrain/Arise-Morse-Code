using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour {

	public float removeSelfTimer = 15f;
	public float letterCountdown = 0.08f;
	[TextArea(3, 10)]
	public string dialogueText;

	private bool imageIsTalking = false;
	private float punctuationPauseCountdown = 0f;
	private bool isWritingDialogue = false;
	private float internalLetterTimer;
	private int currentIndex;

	private void Start() {
		StartDialogueScene();
	}
	private void Update () {
		if (isWritingDialogue == false) {
			return;
		}
		if (dialogueText.Length <= 0) {
			isWritingDialogue = false;
		}
		// We don't use IEnumorators;
		internalLetterTimer += Time.deltaTime;
		
		if (internalLetterTimer > letterCountdown + punctuationPauseCountdown) {
			
			if (dialogueText.Length == 0) {
				isWritingDialogue = false;
				imageIsTalking = false;
				return;
			}

			DisplayFirstLetter();
			internalLetterTimer = internalLetterTimer - (letterCountdown + punctuationPauseCountdown); // reset our timer to 0 plus leftover from last countdown
			punctuationPauseCountdown = 0f;
		}

		if (internalLetterTimer > removeSelfTimer) {

			transform.parent.GetComponent<EntityFade>().StartFadeOut();
		}
	}
	
	public void StartDialogueScene() {
		internalLetterTimer = 0;
		GetComponent<Text>().text = "";
		isWritingDialogue = true;
	} 
	private void DisplayFirstLetter () {

		if (dialogueText.StartsWith("[talk=")) {
			if (dialogueText.Substring(7, dialogueText.Length).StartsWith("overseer-normal]")) {
				//print ();
			}
		}

		if (dialogueText.StartsWith(",")) {
			punctuationPauseCountdown = 0.5f;
			imageIsTalking = false;
		}
		else if (dialogueText.StartsWith(";")) {
		
			punctuationPauseCountdown = 0.5f;
			imageIsTalking = false;
		}
		else if (dialogueText.StartsWith("?!")) {

			GetComponent<Text>().text = GetComponent<Text>().text + dialogueText[0] + dialogueText[1];
			dialogueText = dialogueText.Remove(0, 2);
			punctuationPauseCountdown = 1f;
			imageIsTalking = false;

			return;

		}
		else if (dialogueText.StartsWith("!")) {
		
			punctuationPauseCountdown = 1f;
			imageIsTalking = false;
		}
		else if (dialogueText.StartsWith("?")) {
		
			punctuationPauseCountdown = 1f;
			imageIsTalking = false;
		}
		else if (dialogueText.StartsWith(".")) {
			punctuationPauseCountdown = 1f;
			
			imageIsTalking = false;
		}
		/*
		if (dialogueText.StartsWith("<Color=red>")) {

			for (int i = 0; i < "<Color=red>".Length; i+= 1) {

				GetComponent<Text>().text = GetComponent<Text>().text + dialogueText[i];
			}
			dialogueText = dialogueText.Remove(0, "<Color=red>".Length);
		}
		if (dialogueText.StartsWith("</Color>")) {

		}
		*/
		if (GetComponent<Text>().text.Contains("/") == true && GetComponent<Text>().text.Contains("\\") == true) {

			GetComponent<Text>().text = GetComponent<Text>().text.Replace("/", "<Color=red>");
			GetComponent<Text>().text = GetComponent<Text>().text.Replace("\\", "</Color>");
		}

		//if (dialogueText.StartsWith("[wait=")) {
		//
		//	punctuationPauseCountdown = ;
		//	imageIsTalking = false;
		//}

		//if (dialogueText.StartsWith("[lcd=")) {
		//
		//	letterCountdown = 0.1f;
		//}
		/*
		if (dialogueText.StartsWith("[snd=")) {
			GetComponent<AudioSource>().PlayOneShot();
		}
		*/

		GetComponent<Text>().text = GetComponent<Text>().text + dialogueText[0];
		dialogueText = dialogueText.Remove(0, 1);

		imageIsTalking = true;
		currentIndex += 1;
		if (currentIndex % 2 == 0 && currentIndex < 10) {
			GetComponent<AudioSource>().Play();
		}
	}
}
