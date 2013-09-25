using UnityEngine;
using System.Collections;

[System.Serializable]
public class EasyShuriken {
	public ParticleSystem shuriken;
	public int nParticles { get; private set; }
	public ParticleSystem.Particle[] particles { get; private set; }
	
	public EasyShuriken() {
		nParticles = 0;
		particles = new ParticleSystem.Particle[nParticles];
	}
	
	public void Update() {
		if (nParticles < shuriken.particleCount) {
			particles = new ParticleSystem.Particle[shuriken.particleCount * 2];
		}
		nParticles = shuriken.GetParticles(particles);
	}
	public void Apply() {
		shuriken.SetParticles(particles, nParticles);
	}
}
