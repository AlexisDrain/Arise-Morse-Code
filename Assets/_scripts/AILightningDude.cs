using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILightningDude : MonoBehaviour {

	public string moveTo = "SW";
	public Vector3 targetPos;

	private Transform myTransform;
	private void OnEnable () {

		myTransform = transform;

		targetPos = myTransform.position;

		GameManager.onNewTurn.AddListener(OnNewTurn);
	}

	private void OnDisable() {

		GameManager.onNewTurn.RemoveListener(OnNewTurn);

	}
	private string RecochetDirection() {

		string recochetMoveTo = "";
		// v
		if (moveTo.Contains("N")) {
			recochetMoveTo = recochetMoveTo + "S";
		} else if (moveTo.Contains("S")) {
			recochetMoveTo = recochetMoveTo + "N";
		}

		//h
		if (moveTo.Contains("E")) {
			recochetMoveTo = recochetMoveTo + "W";
		} else if (moveTo.Contains("W")) {
			recochetMoveTo = recochetMoveTo + "E";
		}

		return recochetMoveTo;
	}
	private string GetNextClockwiseDirection() {

		if (moveTo == "SW") {
			return "NW";
		}
		if (moveTo == "NW") {
			return "NE";
		}
		if (moveTo == "NE") {
			return "SE";
		}
		if (moveTo == "SE") {
			return "SW";
		}
		return moveTo;
	}

	private int turnRecursiveDepth = 0;

	private void OnNewTurn() {

		Vector3 movementDirection = new Vector3(0f, 0f);

		if (moveTo.Contains("N")) {
			movementDirection += new Vector3(0f, 1f);
		}
		if (moveTo.Contains("S")) {
			movementDirection += new Vector3(0f, -1f);
		}
		if (moveTo.Contains("E")) {
			movementDirection += new Vector3(1f, 0f);
		}
		if (moveTo.Contains("W")) {
			movementDirection += new Vector3(-1f, 0f);
		}

		RaycastHit hit;
		Physics.Linecast(myTransform.position + new Vector3(0f, 0f, 0.5f), myTransform.position + new Vector3(0f, 0f, 0.5f) + movementDirection, out hit, 1 << GameManager.wallsLayerMask);
		
		if (hit.collider != null) {
			turnRecursiveDepth += 1;
			moveTo = GetNextClockwiseDirection();
			OnNewTurn();
			return;
		}
		if (turnRecursiveDepth > 3) {
			return;
		}
		turnRecursiveDepth = 0;


		TranslateEntity(movementDirection);
	}
	
	public void PositionEntity(Vector3 newPos) {

		targetPos = newPos;
		myTransform.position = newPos;
		myTransform.Find("Trail").GetComponent<TrailRenderer>().Clear();
	}
	public void TranslateEntity(Vector3 translate) {
		

		Vector2 newPos = targetPos + translate;
		int layerMask = (1 << GameManager.wallsLayerMask) + (1 << GameManager.usableWallLayerMask);

		RaycastHit hit;
		Physics.Linecast(myTransform.position + new Vector3(0f, 0f, -0.5f), newPos, out hit, layerMask);
		if (hit.collider == null) {

			targetPos = targetPos + translate;
		}
	}

	private void Update () {
		if (Vector3.Distance(myTransform.position, targetPos) > Mathf.Epsilon) {

			myTransform.position = Vector3.Lerp(myTransform.position, targetPos, 0.5f);
		}
	}
}
