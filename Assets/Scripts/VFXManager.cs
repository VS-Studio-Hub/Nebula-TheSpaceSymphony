using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    public GameObject BlackHoles;
    public VisualEffect Spark;
    public ParticleSystem GreenSpark;

    public GameObject[] laneVFXTransform;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Instantiate(Spark, laneVFXTransform[0].transform.position, Quaternion.identity);
        Spark.Play();
    }


}
