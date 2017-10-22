using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRecochet : MonoBehaviour {


	public int defaultRecochetHP = 2;

	private int recochetHP;
	private Rigidbody myRigidbody;

	private void OnEnable() {
		recochetHP = defaultRecochetHP;

		myRigidbody = GetComponent<Rigidbody>();
	}
	private void OnDisable() {
		StopAllCoroutines();
	}

	IEnumerator RecochetDelete() {

		yield return new WaitForSeconds(0.1f);

		GetComponent<ObjectDeactivater>().DeactivateObject();
	}
	private void OnCollisionEnter (Collision collision) {

		StartCoroutine("RecochetDelete");

		recochetHP -= 1;

		if (recochetHP <= 0 || collision.collider.GetComponent<Health>()) {
			GetComponent<ObjectDeactivater>().DeactivateObject();
		}
	}
}
