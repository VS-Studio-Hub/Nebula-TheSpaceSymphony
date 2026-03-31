using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlanetTransition : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = -1.0f;

    private Material[] material;

    private void Awake()
    {
        material = GetComponent<Renderer>().materials;
        //material.SetFloat("_CutOffHeight", 5);

    }

    private void Update()
    {
        var time = Time.time * Mathf.PI * 0.25f;

        float height = -2;
        height += time * (objectHeight / 2.0f);
        SetHeight(height);
    }

    private void SetHeight(float height)
    {
        material[0].SetFloat("_CutOffHeight", height);
        material[0].SetFloat("_NoiseStrength", noiseStrength);
    }
}
