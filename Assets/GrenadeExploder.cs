using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExploder : MonoBehaviour {

	public float timeUntilExplodeDefault = 1.5f;
	private float timeUntilExplode;
	
	private void OnEnable () {
		timeUntilExplode = timeUntilExplodeDefault;
	}
	

	private void Update () {

		timeUntilExplode -= Time.deltaTime;
		if (timeUntilExplode <= 0) {
			GameManager.explosionsPool.Spawn(transform.position);
			gameObject.SetActive(false);
		}
	}
}
