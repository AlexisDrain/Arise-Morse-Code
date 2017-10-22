using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILightningDude : MonoBehaviour {

	public Vector3 targetPos = new Vector3(0f, 0f);
	public string role;

	public bool stunned;
	private bool noticedPlayer = false;
	private bool playerWithinSight = false;

	private Vector3 startPoint;
	private Transform myTransform;
	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;
	private NavMeshAgent myNavMeshAgent;

	private void Start () {

		myTransform = transform;
		mySpriteRenderer = myTransform.Find("Img").GetComponent<SpriteRenderer>();
		myAnimator = myTransform.Find("Img").GetComponent<Animator>();
		myNavMeshAgent = GetComponent<NavMeshAgent>();

		stunned = false;

		startPoint = myTransform.position;

		SetRole(role);
	}

	public IEnumerator Stun(float timeTillEnd) {

		stunned = true;
		StopAllCoroutines();

		yield return new WaitForSeconds(timeTillEnd);

		stunned = false;
	}

	private void SetRole(string newRole) {

		if (newRole == "Aggressor") {
			myNavMeshAgent.stoppingDistance = 5f;
		}
		else if (newRole == "Flanker") {
			myNavMeshAgent.stoppingDistance = 5f;
		}
		else if (newRole == "Sniper") {
			myNavMeshAgent.stoppingDistance = 10f;
		}
		else if (newRole == "Coward") {
			myNavMeshAgent.stoppingDistance = 5f;
		}
		else if (newRole == "Patrol") {
			myNavMeshAgent.stoppingDistance = 0;
			myNavMeshAgent.SetDestination(targetPos);
		}
	}

	private void Update() {
		//if (noticedPlayer == true && playerWithinSight == false) {
		//}
		if (noticedPlayer == true) {
			
			if (role == "Patrol") {
				SetRole("Aggressor");
			}
			if (role == "Aggressor") {
				targetPos = GameManager.playerTransform.position;
			}
			else if (role == "Flanker") {
				targetPos = GameManager.playerTransform.position + new Vector3(5f, 0f, 0f);
			}
			else if (role == "Sniper") {
				targetPos = GameManager.playerTransform.position;
			} else if (role == "Coward") {
				targetPos = new Vector3(-GameManager.playerTransform.position.x, 0f, -GameManager.playerTransform.position.z);
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

		// visual shite

		if (myNavMeshAgent.velocity.x < 0) {
			mySpriteRenderer.flipX = true;
		}
		else if (myNavMeshAgent.velocity.x > 0) {
			mySpriteRenderer.flipX = false;
		}
		else { // no velocity

			if (playerWithinSight == true) {
				if (GameManager.playerTransform.position.x > transform.position.x) {
					mySpriteRenderer.flipX = false;
				}
				else {
					mySpriteRenderer.flipX = true;
				}
			}
		}

		if (myNavMeshAgent.velocity.magnitude > 1f) {
			myAnimator.SetBool("IsMoving", true);
		} else {
			myAnimator.SetBool("IsMoving", false);

			// since patroling is just visual fluff, I'll leave this logic here.
			if (role == "Patrol") { // and stopped
				if (Vector3.Distance(myNavMeshAgent.destination, myTransform.position) < 1f) {

					if (myNavMeshAgent.destination == startPoint) {
						myNavMeshAgent.SetDestination(targetPos);
					} else {
						myNavMeshAgent.SetDestination(startPoint);
					}
				}
			}
		}
	}
}
