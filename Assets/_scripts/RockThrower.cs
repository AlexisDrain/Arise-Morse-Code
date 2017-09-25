using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour {

	public GameObject itemToThrow;
	public float throwForce = 5f;

	// Use this for initialization
	private void Start() {
		
	}

	// Use this for initialization
	private void UseItem () {
		GameObject temp = GameObject.Instantiate(itemToThrow);

		temp.transform.position = GameManager.player.transform.position;
		temp.GetComponent<Rigidbody>().AddForce(GameManager.player.GetComponent<PlayerMovement>().direction * throwForce);
	}
	
}
