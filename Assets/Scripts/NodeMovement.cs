using UnityEngine;

public class NodeMovement : MonoBehaviour
{
    public float beatTempo;

    void Update()
    {
        transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
    }
}
