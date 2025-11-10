using UnityEngine;

public class NodeMovement : MonoBehaviour
{
    public float beatTempo;      
    public bool hasStarted;      

    void Start()
    {
        //beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        //    
        //if (!hasStarted)
        //{

        //        //hasStarted = true;

        //}
        //else
        //{
        //    transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        //}
    }
}
