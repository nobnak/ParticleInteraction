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

	void Start () {
		_souls = new HashSet<Soul>();
		StartCoroutine("Generator");
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
		_souls.Remove(soul);
	}
}
