using System.Collections;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    private Transform transform1;
    public Vector3 newScale;
    private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform1 == null)
        {
            transform1 = GetComponent<Transform>();

        }
        newScale = this.transform1.localScale;
        //newScale.z,x = Random.Range(0.04f, 0.1f);

        //this.transform1.localScale = newScale;
        if (material == null)
        {
            material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
        }
    }
    void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "StarModel")
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0, Space.Self);
        }
        else
        {
            StartCoroutine(Rotation());
        }
        if ((newScale.z <= this.transform.localScale.z & newScale.z > 0.007f))
        {
            newScale.z = 0.004f;

        }
        if ((newScale.z >= this.transform.localScale.z & newScale.z < 0.007f))
        {
            newScale.z = 0.01f;
        }
        if (!(newScale.z == this.transform.localScale.z))
        {
            Vector3 Addition = new Vector3(0.00055f, 0.00055f, 0.00055f);
            if (newScale.z > this.transform.localScale.z)
            {
                this.transform.localScale += Addition * Time.deltaTime * 10; //* Random.Range(3, 6);
                material.SetFloat("_outline", this.transform.localScale.z*80000);
            }
            else
            {
                this.transform.localScale -= Addition * Time.deltaTime * 10; //* Random.Range(3, 6);
                material.SetFloat("_outline", this.transform.localScale.z*80000);
            }
        }

    }
    IEnumerator Rotation()
    {
        yield return new WaitForSecondsRealtime(0);
        transform.Rotate(0, -100* Time.deltaTime, 0, Space.Self);
    }

}
