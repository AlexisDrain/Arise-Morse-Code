using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public string thisFaction;
	public float healthPoints;

	public void AddHeatlh(float hitPoints, string faction = "") {

		if (thisFaction == "Congregate") {
			
			healthPoints += hitPoints;
			if (hitPoints < 0) {
				StartCoroutine("FlashRed");
			}
		}

		if (healthPoints <= 0) {
			if (thisFaction == "Congregate") {

				gameObject.SetActive(false);
			}

		}
	}


	
	private IEnumerator FlashRed () {
		SpriteRenderer imageSprite = transform.Find("Img").GetComponent<SpriteRenderer>();
		imageSprite.color = Color.red;

		yield return new WaitForSeconds(0.1f);

		imageSprite.color = Color.white;
	}
}
