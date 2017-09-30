using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour {

	public Sprite idle;
	public Sprite dot;
	public Sprite dash;

	private int currentCrosshairStatus = 0;

	private RectTransform myRectTransform;


	private void Start () {
		Cursor.visible = false;
	}


	public void SetCrosshairGraphic(int status) {
		if (currentCrosshairStatus != status) {
			currentCrosshairStatus = status;

			if (status == 0) {
				GetComponent<Image>().sprite = idle;
			} else if (status == 1) {
				GetComponent<Image>().sprite = dot;
			} else if (status == 2) {
				GetComponent<Image>().sprite = dash;
			}
		}
	}

	private void LateUpdate () {
		GetComponent<RectTransform>().transform.position = Input.mousePosition;

		if (Cursor.visible == true) {
			Cursor.visible = false;
		}
	}
}
