using UnityEngine;

public class PurpleNoteMaterial : MonoBehaviour
{
    public bool laneOne, laneTwo, laneThree, laneFour;
    private Renderer rend;
    public Material defaultMaterial;
    Material AdjustMaterial;
    public float Intensity = 10f;
    public float DimSpeed = 0.5f;
    void Awake()
    {

    }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = defaultMaterial;
        AdjustMaterial = rend.material;
    }
    void Update()
    {
        if (GameManager.instance.activatePurpleNote)
        {
            rend.material = MaterialManager.instance.GetCurrentMaterial();
        }
        else
        {
            rend.material = defaultMaterial;
            AdjustMaterial.EnableKeyword("_EmissiveIntensity");
            AdjustMaterial.SetColor("_EmissiveColor", Color.purple * Intensity);
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
}
