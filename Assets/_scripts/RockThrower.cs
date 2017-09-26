using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour {

	public GameObject itemToThrow;
	public float throwImpulse = 5f;
	
	// Use this for initialization
	public void UseItem () {
		GameObject temp = GameObject.Instantiate(itemToThrow);

		temp.transform.position = GameManager.player.transform.position;
		temp.GetComponent<Rigidbody>().AddForce(GameManager.player.GetComponent<PlayerMovement>().throwDirection * throwImpulse, ForceMode.Impulse);

		Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

		StartCoroutine(RestartPlayerItemCollision(temp));
	}

	IEnumerator RestartPlayerItemCollision (GameObject thrownObject) {
		yield return new WaitForSeconds(0.1f);

		Physics.IgnoreCollision(thrownObject.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), false);
	}
	
}
