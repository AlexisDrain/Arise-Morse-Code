using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DisplayCommandCharacters : MonoBehaviour {
	
	public string morseInput = ".-";

	public UnityEvent onCommandUse;

	public void UseCommand () {
		onCommandUse.Invoke();
	}

	private void Start () {

		GameObject temp;
		Transform dashDotTrans = transform.Find("DashDot");

		for (int i = 0; i < morseInput.Length; i += 1) {

			if (morseInput[i].ToString() == ".") {

				temp = GameObject.Instantiate(GameManager.gameManagerGameObject.GetComponent<GameManager>().dot);

				temp.transform.SetParent(dashDotTrans);
				//temp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
				temp.GetComponent<RectTransform>().localScale = Vector3.one; // this needed for resolution scale
				temp.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 10f);
				temp.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
				temp.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);

			} else {

				temp = GameObject.Instantiate(GameManager.gameManagerGameObject.GetComponent<GameManager>().dash);

				temp.transform.SetParent(dashDotTrans);
				//temp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
				temp.GetComponent<RectTransform>().localScale = Vector3.one; // this needed for resolution scale
				temp.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 10f);
				temp.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
				temp.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			}
		}
	}
	
}
