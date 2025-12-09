using UnityEngine;

public class NoteMaterial : MonoBehaviour
{
    public bool laneOne, laneTwo, laneThree, laneFour;
    private Renderer rend;
    public Material[] defaultMaterial;
    void Awake()
    {

    }
    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (laneOne)
            rend.material = defaultMaterial[0];
        if (laneTwo)
            rend.material = defaultMaterial[1];
        if (laneThree)
            rend.material = defaultMaterial[2];
        if (laneFour)
            rend.material = defaultMaterial[3];
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
                rend.material = defaultMaterial[0];
            if (laneTwo)
                rend.material = defaultMaterial[1];
            if (laneThree)
                rend.material = defaultMaterial[2];
            if (laneFour)
                rend.material = defaultMaterial[3];
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
