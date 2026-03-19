using UnityEngine;

public class Move1 : MonoBehaviour
{
    //Transform transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (transform == null)
        //{
        //    transform = this.GetComponent<Transform>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, 0.01f) * Time.deltaTime;
        transform.Rotate(0, -0.01f * Time.deltaTime, 0);
    }
}
