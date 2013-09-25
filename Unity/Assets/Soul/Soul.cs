using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour {
	public System.Action<Soul> OnArrived;

	public float duration;
	[HideInInspector]
	public CoonsCurve curve;
	
	private float _rDuration;
	private float _startTime;
	private State _state;
	
	enum State { Play = 0, BeingDestroyed }

	// Use this for initialization
	void Start () {
		_state = State.Play;
		_rDuration = 1f / duration;
		_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (_state != State.Play)
			return;
		
		var t = (Time.time - _startTime) * _rDuration;
		var pos = curve.Interpolate(t);
		transform.position = pos;
		if (t >= 1f) {
			_state = State.BeingDestroyed;
			if (OnArrived != null)
				OnArrived(this);
			Destroy(gameObject, duration);
		}
	}
}
