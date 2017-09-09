using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Vector3 targetPos;

	private Transform myTransform;
	private void Start () {

		GameManager.players.Add(gameObject);
		myTransform = transform;

		targetPos = myTransform.position;
	}

	public void TranslatePlayer(Vector3 translate) {
		targetPos = targetPos + translate;
	}
	private void Update () {
		if (Vector3.Distance(myTransform.position, targetPos) > Mathf.Epsilon) {

			myTransform.position = Vector3.Lerp(myTransform.position, targetPos, 0.5f);
		}
	}
}
