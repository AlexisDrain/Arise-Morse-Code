using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILightningDude : MonoBehaviour {
	
	public string role;

	private bool noticedPlayer = false;
	private bool playerWithinSight = false;
	private Vector3 targetPos = new Vector3(0f, 0f);

	private Transform myTransform;
	private SpriteRenderer mySpriteRenderer;
	private NavMeshAgent myNavMeshAgent;

	private void Start () {

		myTransform = transform;
		mySpriteRenderer = myTransform.Find("Img").GetComponent<SpriteRenderer>();
		myNavMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	private void Update() {
		if (noticedPlayer == true && playerWithinSight == false) {

		}
		if (noticedPlayer == true) {

			if (role == "Aggressor") {
				targetPos = GameManager.playerTransform.position;
			}
			else if (role == "Flanker") {
				targetPos = GameManager.playerTransform.position + new Vector3(3f, 0f, 0f);
			}
			else if (role == "Coward") {
				targetPos = GameManager.playerTransform.position + new Vector3(0f, 0f, -3f);
			}
			Debug.DrawLine(transform.position, targetPos, Color.green);
			myNavMeshAgent.SetDestination(targetPos);
		}

		RaycastHit hit;
		Physics.Linecast(transform.position, GameManager.playerTransform.position, out hit);

		if (hit.collider && hit.collider.tag == "Player") {


			if (noticedPlayer == false) {
				noticedPlayer = true;
			}

			playerWithinSight = true;
		} else {
			playerWithinSight = false;
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
