using UnityEngine;

public class PurpleNoteMaterial : MonoBehaviour
{
    public bool laneOne, laneTwo, laneThree, laneFour;
    private Renderer rend;
    public Material defaultMaterial;
    void Awake()
    {

    }
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = defaultMaterial;
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
