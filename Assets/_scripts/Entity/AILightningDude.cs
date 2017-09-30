using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILightningDude : MonoBehaviour {
	
	public Vector3 targetPos;

	private bool noticedPlayer = false;
	private bool playerWithinSight = false;

	private Transform myTransform;
	private SpriteRenderer mySpriteRenderer;

	private void Start () {

		myTransform = transform;
		mySpriteRenderer = myTransform.Find("Img").GetComponent<SpriteRenderer>();
		//GetComponent<NavMeshAgent>().SetDestination(targetPos);
	}
	
	private void Update() {
		if (noticedPlayer == true && playerWithinSight == false) {

			GetComponent<NavMeshAgent>().SetDestination(targetPos);
		}

		RaycastHit hit;
		Physics.Raycast(transform.position, GameManager.playerTransform.position, out hit);

		if (hit.collider && hit.collider.tag == "Player") {
			noticedPlayer = true;
			playerWithinSight = true;
		}

		if (playerWithinSight == true) {
			if (GameManager.playerTransform.position.x < myTransform.position.x) {
				mySpriteRenderer.flipX = true;
			} else {
				mySpriteRenderer.flipX = false;
			}
		}
	}
	
}
