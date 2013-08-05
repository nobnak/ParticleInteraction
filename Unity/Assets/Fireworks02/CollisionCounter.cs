using UnityEngine;
using System.Collections;

public class CollisionCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Collisions : " + particleSystem.particleCount);
	}
}
