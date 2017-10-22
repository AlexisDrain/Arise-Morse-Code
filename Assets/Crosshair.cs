using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour {

	//public Sprite idle;
	public Sprite dot;
	public Sprite dash;

	private int currentCrosshairStatus = 0;

	private LineRenderer playerCrosshairLine;
	private Image dotDisplay;
	private RectTransform myRectTransform;

	private void Start () {

		Cursor.visible = false;
		StartCoroutine(HideCursor());

		dotDisplay = transform.GetChild(0).GetComponent<Image>();

		myRectTransform = GetComponent<RectTransform>();
		playerCrosshairLine = GameManager.playerTransform.Find("LineToCrosshair").GetComponent<LineRenderer>();
	}
	
	public void SetCrosshairGraphic(int status) {
		if (currentCrosshairStatus != status) {
			currentCrosshairStatus = status;

			if (status == 0) {
				Color temp = dotDisplay.color;
				temp.a = 0;
				dotDisplay.color = temp;
				dotDisplay.sprite = null;
			} else if (status == 1) {

				Color temp = dotDisplay.color;
				temp.a = 1;
				dotDisplay.color = temp;

				dotDisplay.sprite = dot;
			} else if (status == 2) {

				Color temp = dotDisplay.color;
				temp.a = 1;
				dotDisplay.color = temp;

				dotDisplay.sprite = dash;
			}
		}
	}

	IEnumerator HideCursor () {
		while (true) {

			yield return new WaitForSecondsRealtime(2f);

			if (Cursor.visible == true) {
				Cursor.visible = false;
			}
		}
	}

	private Ray mouseRay;
	private RaycastHit mouseHit, lineHit;
	public Vector3 playerTarget;
	private float playerheight;
	private void LateUpdate () {
		// crosshair pos
		myRectTransform.position = Input.mousePosition;

		// player target point projected on 45 degree ground.
		
		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(mouseRay, out mouseHit, 50f, (1 << GameManager.floorLayerMask));
		if (mouseHit.collider != null) {
			playerTarget = mouseHit.point + new Vector3(0f, GameManager.playerBoxCollider.bounds.center.y, 0f);
		}
		

		// red line of throw
		Physics.Raycast(GameManager.playerBoxCollider.bounds.center, playerTarget - GameManager.playerBoxCollider.bounds.center, out lineHit, 50f, ~(1 << GameManager.floorLayerMask));
		
		if (lineHit.collider != null) {
			playerCrosshairLine.SetPosition(0, GameManager.playerBoxCollider.bounds.center);
			playerCrosshairLine.SetPosition(1, lineHit.point);
		}
		// else {
		// 	 playerCrosshairLine.SetPosition(1, GameManager.playerTransform.InverseTransformPoint(playerTarget));
		// }
	}
}
