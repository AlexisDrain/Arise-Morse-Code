using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILightningDude : MonoBehaviour {
	
	public Vector3 targetPos;

	//private Transform myTransform;

	private void Start () {

		//myTransform = transform;
		
		GetComponent<NavMeshAgent>().SetDestination(targetPos);
	}
	
	//private string RecochetDirection() {

	//}
	
}
