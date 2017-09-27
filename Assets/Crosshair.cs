using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour {

	private RectTransform myRectTransform;
	private void Start () {
		
	}
	

	private void Update () {
		GetComponent<RectTransform>().transform.position = Input.mousePosition;
	}
}
