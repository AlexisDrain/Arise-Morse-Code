using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILightningDude : MonoBehaviour {
	
	public Vector3 targetPos;

	private bool noticedPlayer = false;
	private bool playerWithinSight = false;

	//private Transform myTransform;

	private void Start () {

		//myTransform = transform;
		
		//GetComponent<NavMeshAgent>().SetDestination(targetPos);
	}
	
	private void Update() {
		if (noticedPlayer == true && playerWithinSight == false) {

			GetComponent<NavMeshAgent>().SetDestination(targetPos);
		}

		Physics.Raycast(transform.position, GameManager.player.transform.position);
	}
	
}
