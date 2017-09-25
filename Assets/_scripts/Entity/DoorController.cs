using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public Vector3 openState;

	public bool open = false;
	public bool locked = false;
	public string keyNeeded = "";

	public int turnsUntilCloseDefault = 3;
	private int turnsUntilClose = 3;

	private Transform myTransform;
	private Vector3 targetPos;

	private void Start() {

		myTransform = transform;

		targetPos = myTransform.position;
	}

	private void OnNewTurn() {
		
		if (open == true) {
			turnsUntilClose -= 1;
		} else {
			turnsUntilClose = turnsUntilCloseDefault;
		}

		if (turnsUntilClose <= 0) {
			CloseDoor();
		}
	}
	// boxCollider
	private void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.tag == "Player") {

		}
	}
	private void OpenDoor() {

		open = true;
	}
	private void CloseDoor() {
		open = false;
	}

	private void Update() {
		if (Vector3.Distance(myTransform.position, targetPos) > Mathf.Epsilon) {

			myTransform.position = Vector3.MoveTowards(myTransform.position, targetPos, 0.2f);
		}
	}
}
