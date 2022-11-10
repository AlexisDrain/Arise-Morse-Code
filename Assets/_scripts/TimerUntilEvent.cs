using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TimerUntilEvent : MonoBehaviour {

	public bool loopTimer = false;
	public bool startCountdownOnAwake = true;
	public float countdownTime = 3;
	public UnityEvent events;
	
	private void OnEnable() {
		Debug.LogWarning("TimerUntilEvent is function is depreicated. Use Unity function");
		if (startCountdownOnAwake == true) {
			StartTimer ();
		}
	}

	public void StartTimer () {
		StartCoroutine (TimeCountdown());
	}

	private void ExecuteEvents () {
		StopAllCoroutines();
		events.Invoke ();
		if (loopTimer == true) {
			StartTimer();
		}
	}
	
	// Update is called once per frame
	IEnumerator TimeCountdown () {
		yield return new WaitForSeconds (countdownTime);
		ExecuteEvents ();
	}
}
