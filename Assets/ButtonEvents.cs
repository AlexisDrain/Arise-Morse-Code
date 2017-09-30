using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler {

	/*
	* why
	* because unity Button sprites are buggy.
	*/

	public Sprite onPointerEnter;
	public Sprite onPointerExit;
	public Sprite onPointerClick;
	public Sprite onPointerDown;

	public AudioClip buttonDownSound;
	private Text childText;
	private Color defaultTextColor;

	private void Start() {
		childText = transform.GetChild(0).GetComponent<Text>();
		defaultTextColor = childText.color;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		childText.color = Color.black;
		GetComponent<Image>().sprite = onPointerEnter;
	}

	public void OnPointerExit(PointerEventData eventData) {
		childText.color = defaultTextColor;
		GetComponent<Image>().sprite = onPointerExit;
	}
	public void OnPointerClick(PointerEventData eventData) {
		childText.color = Color.black;
		GetComponent<Image>().sprite = onPointerClick;
	}


	public void OnPointerDown(PointerEventData eventData) {
		childText.color = defaultTextColor;
		GetComponent<AudioSource>().PlayOneShot(buttonDownSound);
		GetComponent<Image>().sprite = onPointerDown;
	}

}
