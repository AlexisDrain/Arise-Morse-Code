using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour {

	public string faction;
	public float hitPoints;
	public float stunTime;

	private void Start () {
		
		//myTransform = transform;
	}
	


	private void OnCollisionEnter (Collision other) {

		if (faction == "Rock") {
			if (GetComponent<Rigidbody>().velocity.magnitude < 1f) {
				return;
			}
		}

		if (other.collider.GetComponent<Health>()) {
			other.collider.GetComponent<Health>().AddHeatlh(hitPoints, faction);

			if (faction == "Rock" && GetComponent<AILightningDude>()) {

				other.collider.GetComponent<AILightningDude>().StartCoroutine("Stun", stunTime);
			}
		}
	}

}
