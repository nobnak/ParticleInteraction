using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class Sweeper : MonoBehaviour {
	public float strength;
	public float radius;
	
	private ParticleSystem _shuriken;
	private ParticleSystem.Particle[] _particles;
	private Vector3 _force;
	private Vector3 _center;
	private float _sqRadius;
	private bool _touching = false;
	private Bounds _bounds;
	
	void Awake() {
		_shuriken = (ParticleSystem) FindObjectOfType(typeof(ParticleSystem));
		_particles = new ParticleSystem.Particle[0];
		_bounds = GetComponent<Collider>().bounds;
		var ext = _bounds.extents;
		ext.z += 1f;
		_bounds.extents = ext;
	}

	// Use this for initialization
	void Start () {
		var pan = GetComponent<PanGesture>();
		pan.StateChanged += delegate(object sender, TouchScript.Events.GestureStateChangeEventArgs e) {
			switch (e.State) {
			case Gesture.GestureState.Began:
			case Gesture.GestureState.Changed:
				Debug.Log("Pan");
				_touching = true;
				_sqRadius = radius * radius;
				_center = pan.WorldTransformCenter;
				_force = strength * pan.WorldDeltaPosition.normalized;
				break;
			default:
				_touching = false;
				break;
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		if (_particles.Length < _shuriken.particleCount) {
			_particles = new ParticleSystem.Particle[_shuriken.particleCount << 1];
		}
		
		var nParticles = _shuriken.GetParticles(_particles);
		
		var dt = Time.deltaTime;
		var rSqRadius = 1.0f / _sqRadius;
		var dtForce = dt * _force;
		var iParticleOut = 0;
		for (int iParticleIn = 0; iParticleIn < nParticles; iParticleIn++) {
			var p = _particles[iParticleIn];
			if (!_bounds.Contains(p.position))
				continue;
			if (!_touching) {
				_particles[iParticleOut++] = p;
				continue;
			}				
			var distVec = p.position - _center;
			var sqDist = distVec.sqrMagnitude;
			if (_sqRadius < sqDist) {
				_particles[iParticleOut++] = p;
				continue;
			}
			var d = sqDist * rSqRadius;
			//p.velocity += Vector3.Lerp(dtForce, Vector3.zero, d);
			p.velocity += dtForce;
			_particles[iParticleOut++] = p;
		}
		
		_shuriken.SetParticles(_particles, iParticleOut);
	}
}
