using UnityEngine;

public class Planet_Spin : MonoBehaviour
{
    private float rotationSpeed = 30f;
    private float timeScale = 1f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * timeScale * Time.deltaTime);
    }
}
