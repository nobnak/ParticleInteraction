using UnityEngine;
using System.Collections;

public class Vacuum : MonoBehaviour {
	public ParticleSystem[] shurikens;

	private Rect _buttonPos = new Rect(10, 10, 150, 50);
		
	void OnGUI() {
		if (GUI.Button(_buttonPos, "Vacuum!")) {
			Debug.Log("Vacuum!");
			foreach (var s in shurikens) {
				s.Stop();
				s.Play();
			}
		}
	}
}
