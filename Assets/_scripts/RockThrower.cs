using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour {

	public string itemToThrow = "Rock";
	public float throwImpulse = 5f;
	
	// Use this for initialization
	public void UseItem () {


		Vector3 throwTarget = Vector3.zero;

		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(mouseRay, out hit, 50f, (1 << GameManager.floorLayerMask));
		if (hit.collider != null) {
			throwTarget = hit.point;
		}

		Vector3 throwDirection = (throwTarget - GameManager.playerTransform.position).normalized;

		if (itemToThrow == "Rock") {

			GameObject temp = GameManager.rocksPool.Spawn(GameManager.playerTransform.position);
			
			temp.GetComponent<Rigidbody>().AddForce(throwDirection * throwImpulse, ForceMode.Impulse);
			Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

			StartCoroutine(RestartPlayerItemCollision(temp));
		} else {
			if (itemToThrow == "Grenade") {

				GameObject temp = GameManager.grenadesPool.Spawn(GameManager.playerTransform.position);
				

				temp.GetComponent<Rigidbody>().AddForce(throwDirection * throwImpulse, ForceMode.Impulse);
				Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

				StartCoroutine(RestartPlayerItemCollision(temp));
			}
		}
	}

	IEnumerator RestartPlayerItemCollision (GameObject thrownObject) {
		yield return new WaitForSeconds(0.1f);

		Physics.IgnoreCollision(thrownObject.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), false);
	}
	
}
