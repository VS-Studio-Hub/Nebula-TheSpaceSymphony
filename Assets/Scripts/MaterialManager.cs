using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager instance;

    public Material[] noteColours;
    public int colourIndex = 0;

    public float colorInterval = 0.2f;
    private float time;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= colorInterval)
        {
            colourIndex++;
            if (colourIndex >= noteColours.Length)
                colourIndex = 0;

            time = 0f;
        }
    }

    public Material GetCurrentMaterial()
    {
        return noteColours[colourIndex];
    }
}
