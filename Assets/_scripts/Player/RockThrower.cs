using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour {

	public string itemToThrow = "Rock";
	public float throwImpulse = 5f;
	
	// Use this for initialization
	public void UseItem () {
		

		if (itemToThrow == "Rock") {

			Vector3 throwDirection = (GameManager.crosshair.playerTarget - GameManager.playerTransform.position).normalized;

			GameObject temp = GameManager.rocksPool.Spawn(GameManager.playerBoxCollider.bounds.center);
			
			temp.GetComponent<Rigidbody>().AddForce(throwDirection * throwImpulse, ForceMode.Impulse);
			Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

			StartCoroutine(RestartPlayerItemCollision(temp));

		} else if (itemToThrow == "Grenade") {

			Vector3 throwDirection = (GameManager.crosshair.playerTarget - GameManager.playerTransform.position).normalized;

			GameObject temp = GameManager.grenadesPool.Spawn(GameManager.playerBoxCollider.bounds.center);
				
			temp.GetComponent<Rigidbody>().AddForce(throwDirection * throwImpulse, ForceMode.Impulse);
			Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

			StartCoroutine(RestartPlayerItemCollision(temp));
		}
		else if (itemToThrow == "Machinegun") {
			StartCoroutine("MachinegunDelay");
		} else if (itemToThrow == "SuperShotgun") {

			Vector3 throwDirection = (GameManager.crosshair.playerTarget - GameManager.playerTransform.position).normalized;

			for (int i = 0; i < 7; i++) {

				Vector3 shotgunThrowPlusOffset = Vector3.Cross(throwDirection, new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0f)).normalized;

				GameObject temp = GameManager.bulletPool.Spawn(GameManager.playerBoxCollider.bounds.center);

				temp.GetComponent<Rigidbody>().AddForce(shotgunThrowPlusOffset * throwImpulse, ForceMode.Impulse);
				Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

				StartCoroutine(RestartPlayerItemCollision(temp));
			}
			
		}
	}

	IEnumerator MachinegunDelay () {
		MachinegunFire();
		yield return new WaitForSeconds(0.1f);
		MachinegunFire();
		yield return new WaitForSeconds(0.1f);
		MachinegunFire();

		yield return new WaitForSeconds(0.3f);
		MachinegunFire();
		yield return new WaitForSeconds(0.1f);
		MachinegunFire();
		yield return new WaitForSeconds(0.1f);
		MachinegunFire();
	}
	private void MachinegunFire() {

		Vector3 throwDirection = (GameManager.crosshair.playerTarget - GameManager.playerTransform.position).normalized;

		GameObject temp = GameManager.bulletPool.Spawn(GameManager.playerBoxCollider.bounds.center);
		temp.GetComponent<Rigidbody>().AddForce(throwDirection * throwImpulse, ForceMode.Impulse);
		Physics.IgnoreCollision(temp.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), true);

		StartCoroutine(RestartPlayerItemCollision(temp));
	}
	IEnumerator RestartPlayerItemCollision (GameObject thrownObject) {
		yield return new WaitForSeconds(0.1f);

		Physics.IgnoreCollision(thrownObject.GetComponent<Collider>(), GameManager.player.GetComponent<Collider>(), false);
	}
	
}
