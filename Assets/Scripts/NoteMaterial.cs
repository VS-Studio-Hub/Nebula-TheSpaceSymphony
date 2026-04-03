using UnityEngine;

public class NoteMaterial : MonoBehaviour
{
    public bool laneOne, laneTwo, laneThree, laneFour;
    private Renderer rend;
    public Material[] defaultMaterial;
    public float Intensity = 10f;
    Material AdjustMaterial;
    public float DimSpeed=0.5f;
    void Awake()
    {

    }
    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (laneOne)
        {
            rend.material = defaultMaterial[0];
            AdjustMaterial = GetComponentInChildren<Renderer>().material;
            AdjustMaterial.EnableKeyword("_EmissiveIntensity");
            rend.material = AdjustMaterial;
        }

        if (laneTwo)
        {
            rend.material = defaultMaterial[1];
            AdjustMaterial = GetComponentInChildren<Renderer>().material;
            AdjustMaterial.EnableKeyword("_EmissiveIntensity");
            rend.material = AdjustMaterial;
        }

        if (laneThree)
        {
            rend.material = defaultMaterial[2];
            AdjustMaterial = GetComponentInChildren<Renderer>().material;
        }

        if (laneFour)
        {
            rend.material = defaultMaterial[3];
            AdjustMaterial = GetComponentInChildren<Renderer>().material;
            AdjustMaterial.EnableKeyword("_EmissiveIntensity");
            rend.material = AdjustMaterial;
        }
        //AdjustMaterial.EnableKeyword("_EmissiveIntensity");

    }
    void Update()
    {   
        
        if (GameManager.instance.activatePurpleNote)
        {
            rend.material = MaterialManager.instance.GetCurrentMaterial();
        }
        else
        {
            if (laneOne)
            {
                rend.material = defaultMaterial[0];
                AdjustMaterial = rend.material;
                AdjustMaterial.EnableKeyword("_EmissiveIntensity");
                AdjustMaterial.SetColor("_EmissiveColor",Color.green * Intensity);
                

            }

            if (laneTwo)
            {
                rend.material = defaultMaterial[1];
                AdjustMaterial = rend.material;
                AdjustMaterial.EnableKeyword("_EmissiveIntensity");
                //AdjustMaterial.SetFloat("_EmissiveIntensity", Intensity);
                AdjustMaterial.SetColor("_EmissiveColor", Color.blue * Intensity);
                
            }

            if (laneThree)
            {
                rend.material = defaultMaterial[2];
                //AdjustMaterial = GetComponentInChildren<Renderer>().material;
                AdjustMaterial = rend.material;
                AdjustMaterial.EnableKeyword("_EmissiveIntensity");
                AdjustMaterial.SetColor("_EmissiveColor", Color.red * Intensity);
                
            }

            if (laneFour)
            {
                rend.material = defaultMaterial[3];
                AdjustMaterial = rend.material;
                AdjustMaterial.EnableKeyword("_EmissiveIntensity");
                //AdjustMaterial.SetFloat("_EmissiveIntensity", Intensity);
                AdjustMaterial.SetColor("_EmissiveColor", Color.yellow * Intensity);
                
            }
            Intensity -= DimSpeed * Time.deltaTime;
            //UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(AdjustMaterial);
            rend.material = AdjustMaterial;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LaneOne"))
            laneOne = true;
        if (other.CompareTag("LaneTwo"))
            laneTwo = true;
        if (other.CompareTag("LaneThree"))
            laneThree = true;
        if (other.CompareTag("LaneFour"))
            laneFour = true;
    }
    public static void UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(Material material)
    {
        const string kEmissiveColorLDR = "_EmissiveColorLDR";
        const string kEmissiveColor = "_EmissiveColor";
        const string kEmissiveIntensity = "_EmissiveIntensity";

        if (material.HasProperty(kEmissiveColorLDR) && material.HasProperty(kEmissiveIntensity) && material.HasProperty(kEmissiveColor))
        {
            // Important: The color picker for kEmissiveColorLDR is LDR and in sRGB color space but Unity don't perform any color space conversion in the color
            // picker BUT only when sending the color data to the shader... So as we are doing our own calculation here in C#, we must do the conversion ourselves.
            Color emissiveColorLDR = material.GetColor(kEmissiveColorLDR);
            Color emissiveColorLDRLinear = new Color(Mathf.GammaToLinearSpace(emissiveColorLDR.r), Mathf.GammaToLinearSpace(emissiveColorLDR.g), Mathf.GammaToLinearSpace(emissiveColorLDR.b));
            material.SetColor(kEmissiveColor, emissiveColorLDRLinear * material.GetFloat(kEmissiveIntensity));
            Debug.Log("Hekllo");
        }
    }
}
