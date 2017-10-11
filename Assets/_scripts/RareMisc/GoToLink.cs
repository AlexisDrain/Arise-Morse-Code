using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoToLink : MonoBehaviour, IPointerClickHandler {

	public bool onlyShowOnWeb = false;
	public string link;

	public void Start() {
		if (onlyShowOnWeb == true && Application.platform != RuntimePlatform.WebGLPlayer) {
			GetComponent<Button>().transform.localScale = new Vector3(0f, 0f, 0f);
		}
	}
	public void OnPointerClick (PointerEventData eventData) {

		if (Application.platform == RuntimePlatform.WebGLPlayer) {

			Application.ExternalEval("window.open('" + link + "');");
		} else {

			Application.OpenURL(link);
		}
	}
	
}
