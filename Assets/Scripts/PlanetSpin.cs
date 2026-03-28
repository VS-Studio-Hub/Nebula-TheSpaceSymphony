using UnityEngine;

public class PlanetSpin : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(20, 20, 0);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
