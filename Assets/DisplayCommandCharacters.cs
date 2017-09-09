using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCommandCharacters : MonoBehaviour {
	
	public int morseIndex;
	
	private void Start () {

		GameObject temp;

		string command = GameManager.interpreter.GetComponent<MorseInput>().ariseAlphabet[morseIndex];

		for (int i = 0; i < command.Length; i += 1) {

			if (command[i].ToString() == ".") {

				temp = GameObject.Instantiate(GameManager.gameManagerGameObject.GetComponent<GameManager>().dot);

				temp.transform.SetParent(transform);
				//temp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
				temp.GetComponent<RectTransform>().sizeDelta = new Vector2(10f, 10f);
				temp.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
				temp.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);

			} else {

				temp = GameObject.Instantiate(GameManager.gameManagerGameObject.GetComponent<GameManager>().dash);

				temp.transform.SetParent(transform);
				//temp.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
				temp.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 10f);
				temp.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
				temp.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			}
		}
	}
	
}
