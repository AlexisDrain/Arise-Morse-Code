using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivater : MonoBehaviour {

	public float autoDestruct = -1f;
	public bool saveTrail = false;
	//private Transform myTransform;

	private GameObject trail;

	private void Start() {
		if (saveTrail == true) {
			trail = transform.Find("Trail").gameObject;
		}
	}
	private void OnEnable() {

		if (transform.Find("Trail") == null) {
			GameObject newTrail = GameObject.Instantiate(trail, transform);
			newTrail.name = "Trail";
			newTrail.transform.position = transform.Find("Img").position;
		}
		if (autoDestruct != -1f) {
			StartCoroutine("AutoDestruct");
		}
	}
	IEnumerator AutoDestruct() {

		yield return new WaitForSeconds(autoDestruct);
		DeactivateObject();
	}
	public void DeactivateObject () {
		if (saveTrail == true) {

			Transform trail = transform.Find("Trail");
			if (trail != null) {
				trail.parent = null;
			}

		}

		if (gameObject.GetComponent<Rigidbody>()) {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		}
		print(gameObject.GetComponent<Rigidbody>().velocity);

		gameObject.SetActive(false);
	}
}
