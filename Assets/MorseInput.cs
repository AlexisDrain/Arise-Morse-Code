using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseInput : MonoBehaviour {


	// also Input.GetButtonDown("SubmitPremature") for entering morse quickly

	private float commandEndTimer;
	private bool commandBeingEntered = false;

	private float buttonHeldTime;
	private bool buttonBeingHeldDown = false;

	private string result = "dot";
	
	
	private void Update () {

		if (Input.GetButton("Jump") == true) {
			commandBeingEntered = true;
			// reset command timer
			commandEndTimer = 0;
		}

		if (Input.GetButtonDown("Jump") == true) {
			buttonBeingHeldDown = true;
		}

		if (Input.GetButtonUp("Jump") == true) {
			buttonBeingHeldDown = false;
			
			if (buttonHeldTime < 0.2f) {
				result = "dot";
			} else {

				result = "dash";
			}

			print(buttonHeldTime + " " + result);
			buttonHeldTime = 0;
			// reset command timer
			commandEndTimer = 0;
		}

		if (buttonBeingHeldDown == true) {

			buttonHeldTime += Time.deltaTime;
		}
		if (commandBeingEntered == true) {

			commandEndTimer += Time.deltaTime;
		}
	}
}
