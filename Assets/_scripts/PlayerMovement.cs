using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementImpulse;

	private Transform myTransform;
	private Rigidbody myRigidbody;
	private void Start () {

		myTransform = transform;
		
		myRigidbody = GetComponent<Rigidbody>();
	}
	/*
	public void PositionPlayer(Vector3 newPos) {

		targetPos = newPos;
		myTransform.position = newPos;
		myTransform.Find("Trail").GetComponent<TrailRenderer>().Clear();
	}
	public void TranslatePlayer(Vector3 translate) {

		Vector3 newPos = targetPos + translate;
		int layerMask = (1 << GameManager.wallsLayerMask) + (1 << GameManager.usableWallLayerMask);

		RaycastHit hit;
		Physics.Linecast(myTransform.position + new Vector3(0f, 0f, 0.4f), newPos + new Vector3(0f, 0f, 0.4f), out hit, layerMask);
		if (hit.collider == null) {

			targetPos = targetPos + translate;
		}
	}
	*/
	/*
private void Update () {
	if (Vector3.Distance(myTransform.position, targetPos) > Mathf.Epsilon) {

		myTransform.position = Vector3.Lerp(myTransform.position, targetPos, 0.5f);
	}

}
	*/

	private void FixedUpdate() {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		if (h != 0) {
			myRigidbody.AddForce(Vector3.right * h * movementImpulse, ForceMode.Impulse);
		}

		if (v != 0) {
			myRigidbody.AddForce(Vector3.up * v * movementImpulse, ForceMode.Impulse);
		}
	}
}
