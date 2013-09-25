using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public float rotSpeed;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation *= Quaternion.AngleAxis(rotSpeed * Time.deltaTime, Vector3.up);
	}
}
