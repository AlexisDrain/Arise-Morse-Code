using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour {

	public RuntimeAnimatorController playerRight;
	public AnimatorOverrideController playerUp;
	public AnimatorOverrideController playerDown;
	public AnimatorOverrideController playerLeft;
	private Vector2 currentDirection;
	private bool isCrawling = false;

	public Vector2 direction = new Vector2(1f, 0f);
	public float movementImpulse;
	
	private Rigidbody myRigidbody;
	private Animator myAnimator;

	private void Start () {
		
		myRigidbody = GetComponent<Rigidbody>();
		myAnimator = transform.Find("Img").GetComponent<Animator>();
	}

	private void FixedUpdate() {

		if (Input.GetButton("KeyCrawl")) {
			isCrawling = true;
			movementImpulse = 2f;
		}
		else {
			isCrawling = false;
			movementImpulse = 3f;
		}
		myAnimator.SetBool("IsCrawling", isCrawling);


		float h = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
		float v = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);

		Vector3 movementNormalized = new Vector3(h, 0, v).normalized;
		

		if (v != 0 || h != 0) {
			myRigidbody.AddForce(movementNormalized * movementImpulse, ForceMode.Impulse);

			myAnimator.SetBool("IsMoving", true);

			currentDirection = new Vector2(h, v);
		} else {

			myAnimator.SetBool("IsMoving", false);
		}

		if (direction != currentDirection) {
			direction = currentDirection;

			// set new Animation Controller
			if (h > 0) {
				myAnimator.runtimeAnimatorController = playerRight;
			}
			else if (h < 0) {
				myAnimator.runtimeAnimatorController = playerLeft;
			}
			else if (v > 0) {
				myAnimator.runtimeAnimatorController = playerUp;
			}
			else if (v < 0) {
				myAnimator.runtimeAnimatorController = playerDown;
			}
		}

		/*
		if (h != 0) {
			myRigidbody.AddForce(Vector3.right * h * movementImpulse, ForceMode.Impulse);

			direction = new Vector2(h, 0f);
			throwDirection = new Vector2(h, 0f);

			//transform.Find("Trail").GetComponent<TrailRenderer>().Clear();
		}
		*/

	}
}
