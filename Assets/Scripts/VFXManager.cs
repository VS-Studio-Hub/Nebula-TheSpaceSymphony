using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.VFX;
using System.Collections;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    public GameObject BlackHoles;
    public VisualEffect Spark;
    public ParticleSystem GreenSpark;

    public GameObject[] laneVFXTransform;

    [SerializeField] private GameObject leftSideBarrier, rightSideBarrier;

    public static bool blackHole, spark, greenSpark;

    private float blackHoleDuration = 0.3f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        leftSideBarrier.SetActive(false);
        rightSideBarrier.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.instance.activatePurpleNote)
        {
            leftSideBarrier.SetActive(true);
            rightSideBarrier.SetActive(true);
        }
        else
        {
            leftSideBarrier.SetActive(false);
            rightSideBarrier.SetActive(false);
        }
        GreenSpark.Play();
    }

    public void LaneOneVFX()
    {
        if (spark)
        {
            VisualEffect clone = Instantiate(Spark, laneVFXTransform[0].transform.position, Quaternion.identity);
            Spark.Play();
            spark = false;
            StartCoroutine(DestroyVisualEffectAfterTime(clone, 2f));
        }
        else if (greenSpark)
        {
            ParticleSystem clone = Instantiate(GreenSpark, laneVFXTransform[0].transform.position, Quaternion.identity);
            GreenSpark.Play();
            greenSpark = false;
            StartCoroutine(DestroyParticleSystemAfterTime(clone, 2f));
        }
        else if (blackHole)
        {
            GameObject clone = Instantiate(BlackHoles, laneVFXTransform[0].transform.position, Quaternion.identity);
            blackHole = false;
            StartCoroutine(DestroyGameObjectAfterTime(clone, blackHoleDuration));
        }
    }
    public void LaneTwoVFX()
    {
        if (spark)
        {
            VisualEffect clone = Instantiate(Spark, laneVFXTransform[1].transform.position, Quaternion.identity);
            Spark.Play();
            spark = false;
            StartCoroutine(DestroyVisualEffectAfterTime(clone, 2f));
        }
        else if (greenSpark)
        {
            ParticleSystem clone = Instantiate(GreenSpark, laneVFXTransform[1].transform.position, Quaternion.identity);
            GreenSpark.Play();
            greenSpark = false;
            StartCoroutine(DestroyParticleSystemAfterTime(clone, 2f));
        }
        else if (blackHole)
        {
            GameObject clone = Instantiate(BlackHoles, laneVFXTransform[1].transform.position, Quaternion.identity);
            blackHole = false;
            StartCoroutine(DestroyGameObjectAfterTime(clone, blackHoleDuration));
        }
    }

    public void LaneThreeVFX()
    {
        if (spark)
        {
            VisualEffect clone = Instantiate(Spark, laneVFXTransform[2].transform.position, Quaternion.identity);
            Spark.Play();
            spark = false;
            StartCoroutine(DestroyVisualEffectAfterTime(clone, 2f));
        }
        else if (greenSpark)
        {
            ParticleSystem clone = Instantiate(GreenSpark, laneVFXTransform[2].transform.position, Quaternion.identity);
            GreenSpark.Play();
            greenSpark = false;
            StartCoroutine(DestroyParticleSystemAfterTime(clone, 2f));
        }
        else if (blackHole)
        {
            GameObject clone = Instantiate(BlackHoles, laneVFXTransform[2].transform.position, Quaternion.identity);
            blackHole = false;
            StartCoroutine(DestroyGameObjectAfterTime(clone, blackHoleDuration));
        }
    }
    public void LaneFourVFX()
    {
        if (spark)
        {
            VisualEffect clone = Instantiate(Spark, laneVFXTransform[3].transform.position, Quaternion.identity);
            Spark.Play();
            spark = false;
            StartCoroutine(DestroyVisualEffectAfterTime(clone, 2f));
        }
        else if (greenSpark)
        {
            ParticleSystem clone = Instantiate(GreenSpark, laneVFXTransform[3].transform.position, Quaternion.identity);
            GreenSpark.Play();
            greenSpark = false;
            StartCoroutine(DestroyParticleSystemAfterTime(clone, 2f));
        }
        else if (blackHole)
        {
            GameObject clone = Instantiate(BlackHoles, laneVFXTransform[3].transform.position, Quaternion.identity);
            blackHole = false;
            StartCoroutine(DestroyGameObjectAfterTime(clone, blackHoleDuration));
        }
    }

    IEnumerator DestroyGameObjectAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    IEnumerator DestroyParticleSystemAfterTime(ParticleSystem ps, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(ps.gameObject);
    }

    IEnumerator DestroyVisualEffectAfterTime(VisualEffect vfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(vfx.gameObject);
    }
}
