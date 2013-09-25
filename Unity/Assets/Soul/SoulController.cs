using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class SoulController : MonoBehaviour {
	public GameObject soulfab;
	public Transform source, target;
	public float interval;
	public float startSpeed;
	public Vector3 endVelocity;
	
	private HashSet<Soul> _souls;
	private float _initialScale, _targetScale;

	void Start () {
		_souls = new HashSet<Soul>();
		StartCoroutine("Generator");
		_initialScale = _targetScale = target.localScale.x;
	}
	
	void Update() {
		target.localScale = _targetScale * Vector3.one;
		_targetScale = Mathf.Lerp(_targetScale, _initialScale, 0.1f);
	}
	
	IEnumerator Generator() {
		while (true) {
			yield return new WaitForSeconds(interval);
			var sourcePos = source.position;
			var soul = ((GameObject)Instantiate(soulfab, sourcePos, Quaternion.identity)).GetComponent<Soul>();
			_souls.Add(soul);
			soul.transform.parent = transform;
			soul.OnArrived += HandleOnArrive;
			soul.curve = new CoonsCurve(sourcePos, target.position, startSpeed * Random.onUnitSphere, endVelocity);
		}
	}
	
	void HandleOnArrive(Soul soul) {
		_targetScale = 2f * _initialScale;
		_souls.Remove(soul);
	}
}
