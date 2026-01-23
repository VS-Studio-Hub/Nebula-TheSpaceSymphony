using UnityEngine;

public class StageVFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 newScale;
    void Start()
    {
        newScale = this.transform.localScale;
        newScale.z = Random.Range(-1.75f, 1.75f);

        this.transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        if ((newScale.z <= this.transform.localScale.z & newScale.z > 0) || (newScale.z >= this.transform.localScale.z & newScale.z < 0))
        {
            newScale.z = Random.Range(-1.75f, 1.75f);
            if (newScale.z < 0.1f &  newScale.z > -0.1f )
            {
                newScale.z = Random.Range(-1.75f, 1.75f);
            }
        }
        else
        {
            Vector3 Addition = new Vector3(0, 0, Random.Range(1f, 5f));
            if (newScale.z >= 0)
            {
                this.transform.localScale += Addition * Time.deltaTime;
            }
            else
            {
                this.transform.localScale -= Addition * Time.deltaTime;
            }

        }
    }
}
