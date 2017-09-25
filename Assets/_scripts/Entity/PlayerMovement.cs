using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Vector2 direction = new Vector2(1f, 0f);
	public float movementImpulse;
	
	private Rigidbody myRigidbody;
	private TrailRenderer myTrailRenderer;

	private void Start () {
		
		myRigidbody = GetComponent<Rigidbody>();
		myTrailRenderer = GetComponent<TrailRenderer>();
	}

	// unity will reset our keys on level change. So workaround:
	private bool keyRightIsPressed = false;

	private void FixedUpdate() {
		float h = Mathf.Clamp(Input.GetAxis("Horizontal") + hardInput.GetAxis("PadLeft", "PadRight"), -1, 1);
		float v = Mathf.Clamp(Input.GetAxis("Vertical") + hardInput.GetAxis("PadUp", "PadDown"), -1, 1);

		if (Input.GetKeyDown("right")) {
			keyRightIsPressed = true;
		} else if (Input.GetKeyUp("right")) {
			keyRightIsPressed = false;
		}

		if (v != 0) {
			myRigidbody.AddForce(Vector3.forward * v * movementImpulse, ForceMode.Impulse);
			
			direction = new Vector2(0f, v);
		}
		if (h != 0) {
			myRigidbody.AddForce(Vector3.right * h * movementImpulse, ForceMode.Impulse);

			direction = new Vector2(h, 0f);

			transform.Find("Trail").GetComponent<TrailRenderer>().Clear();
		}

	}
}
