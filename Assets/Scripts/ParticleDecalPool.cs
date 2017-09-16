using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour {

    public int maxDecals = 10000;
    public float decalSizeMin = .2f;
    public float decalSizeMax = .2f;
    public ParticleSystem.MinMaxCurve minMaxCurve;
    Gradient g;

    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;

    void Start () 
    {
        minMaxCurve = new ParticleSystem.MinMaxCurve(10.0f, 0f);
        decalParticleSystem = GetComponent<ParticleSystem> ();

        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++) 
        {
            particleData [i] = new ParticleDecalData ();    
        }
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData (particleCollisionEvent, colorGradient);
        DisplayParticles ();
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if (particleDecalDataIndex >= maxDecals) 
        {
            particleDecalDataIndex = 0;
        }
            
        particleData [particleDecalDataIndex].position = particleCollisionEvent.intersection - new Vector3(0,0.5f,-1.8f);
        Vector3 particleRotationEuler = Quaternion.LookRotation (particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range (0, 360);
        particleData [particleDecalDataIndex].rotation = particleRotationEuler;
        particleData [particleDecalDataIndex].size = Random.Range (decalSizeMin, decalSizeMax);
        particleData [particleDecalDataIndex].color = colorGradient.Evaluate (Random.Range (0f, 1f));

        particleDecalDataIndex++;
    }

    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++) 
        {
            particles [i].position = particleData [i].position;
            particles [i].rotation3D = particleData [i].rotation;
            particles [i].startSize = particleData [i].size;
            particles [i].startColor = particleData [i].color;
        }

        ParticleSystem.MainModule main = decalParticleSystem.main;

        decalParticleSystem.SetParticles (particles, particles.Length);
    }
}