using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RainbowEffect : MonoBehaviour
{
    private GameObject ScreenEffect;
    private Image image;
    private Color[] colors;
    private Color colorAtOneMoment;
    private MeshRenderer meshRenderer;
    private float hue;
    private float saturation;
    private float brightness;
    public float rainbowSpeed = 10000f;
    public bool Randomizer = true;
    public bool Invert = true;
    public NodeSpawnManager NodeSpawnmanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ScreenEffect = GameObject.Find("Panel");
        //image = ScreenEffect.GetComponent<Image>();


    }


    // Update is called once per frame
    void Update()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (NodeSpawnmanager == null)
        {
            NodeSpawnmanager = GameObject.Find("NodeSpawnManager").GetComponent<NodeSpawnManager>();
        }
        if (NodeSpawnmanager.DoublePointState)
        {
            if (Randomizer)
            {
                hue = Random.Range(0f, 1f);
                saturation = 1;
                brightness = 1;
                meshRenderer.material.color = Color.HSVToRGB(hue, saturation, brightness);
                Randomizer = false;
            }
            Color.RGBToHSV(meshRenderer.material.color, out hue, out saturation, out brightness);
            if (Invert)
            {
                hue -= rainbowSpeed / 10000;
                if (hue < 0)
                {
                    hue = 0.99f;
                }
                //meshRenderer.material.color = Color.HSVToRGB(hue, saturation, brightness);
            }
            else
            {
                hue += rainbowSpeed / 10000;
                if (hue >= 1)
                {
                    hue = 0;
                }
                //meshRenderer.material.color = Color.HSVToRGB(hue, saturation, brightness);
            }
            meshRenderer.material.color = Color.HSVToRGB(hue, saturation, brightness);

        }

    }
}
