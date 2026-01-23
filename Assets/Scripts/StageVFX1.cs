using UnityEngine;

public class StageVFX1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 newScale;
    void Start()
    {
        newScale = this.transform.localScale;
        newScale.y = Random.Range(3.5f, 6.5f);

        this.transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        if ((newScale.y <= this.transform.localScale.y & newScale.y > 5) || (newScale.y >= this.transform.localScale.y & newScale.y < 5))
        {
            newScale.y = Random.Range(3.5f, 6.5f);
            if (newScale.y < 0.1f &  newScale.y > -0.1f )
            {
                newScale.y = Random.Range(3.5f, 6.5f);
            }
        }
        if (!(newScale.y == this.transform.localScale.y))
        {
            Vector3 Addition = new Vector3(0, Random.Range(3f, 5f), 0);
            if (newScale.y > this.transform.localScale.y)
            {
                this.transform.localScale += Addition * Time.deltaTime * Random.Range(3, 6);
            }
            else
            {
                this.transform.localScale -= Addition * Time.deltaTime * Random.Range(3,6);
            }
        }
    }
}
