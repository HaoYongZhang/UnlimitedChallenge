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

        mainModule.startColor = color;
        shapeModule.rotation = new Vector3(-90, 0, 0);

        particleSystem.Play();
    }
}
