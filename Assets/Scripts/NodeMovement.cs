using UnityEngine;

public class NodeMovement : MonoBehaviour
{
    public float beatTempo;
    //public GameObject pathWay;
    void Update()
    {
        transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        //transform.position = Vector3.Lerp(transform.position, pathWay.transform.position, beatTempo);


        if(transform.position.x < -250f)
        {
            Destroy(gameObject);
        }
    }
}
