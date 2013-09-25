using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour {
	public System.Action<Soul> OnArrived;

	public float duration;
	[HideInInspector]
	public CoonsCurve curve;
	
	private float _rDuration;
	private float _startTime;

	// Use this for initialization
	void Start () {
		_rDuration = 1f / duration;
		_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		var t = (Time.time - _startTime) * _rDuration;
		var pos = curve.Interpolate(t);
		transform.position = pos;
		if (t >= 1f) {
			if (OnArrived != null)
				OnArrived(this);
			Destroy(gameObject);
		}
	}
}
