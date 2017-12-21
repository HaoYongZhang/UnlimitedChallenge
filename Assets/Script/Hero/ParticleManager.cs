using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParticleManager {

    public static void vertical(GameObject obj, Color color)
    {
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule mainModule = particleSystem.main;
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;

        mainModule.duration = 1f;
        mainModule.startColor = color;
        mainModule.startSpeed = 20;
        mainModule.startSize = new ParticleSystem.MinMaxCurve(0.1f, 0.6f);
        mainModule.startLifetime = 0.5f;

        shapeModule.rotation = new Vector3(-90, 0, 0);
        shapeModule.angle = 2.2f;

        particleSystem.Play();
    }
}
