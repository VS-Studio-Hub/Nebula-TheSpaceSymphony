using UnityEngine;

public class BlackholeAnim : MonoBehaviour
{
    private Transform transform1;
    public Vector3 newScale;
    private Material material;
    float a = 1f;
    float b = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform1 == null)
        {
            transform1 = GetComponent<Transform>();

        }
        newScale = this.transform1.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, 1000 * Time.deltaTime, 0, Space.Self);
        newScale = new Vector3(a, a, a);
        if (newScale.x == 1)
        {
            if (newScale.x <= this.transform.localScale.x)
            {
                a = 1.5f;
            }
            b += 2000f * Time.deltaTime;
           
        }
        else
        {
            b -=1520f * Time.deltaTime;
        }

        if (!(newScale.x == this.transform.localScale.x))
        {
            Vector3 Addition = new Vector3(0.05f, 0.05f, 0.05f);
            this.transform.localScale += Addition * Time.deltaTime * (1 + b);
        }

    }
}
