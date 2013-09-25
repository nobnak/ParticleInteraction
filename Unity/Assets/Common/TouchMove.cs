using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class TouchMove : MonoBehaviour {
	public float speed;
	
	private PanGesture _pan;

	// Use this for initialization
	void Start () {
		_pan = GetComponent<PanGesture>();
		_pan.StateChanged += delegate(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
			switch (e.State) {
			case Gesture.GestureState.Began:
			case Gesture.GestureState.Changed:
				transform.position += speed * _pan.WorldDeltaPosition;
				break;
			default:
				break;
			}
		};
	}
}
