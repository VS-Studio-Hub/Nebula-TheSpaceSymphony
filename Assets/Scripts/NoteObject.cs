using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;

    private InputAction a_ButtonPressAction;
    private InputAction s_ButtonPressAction;
    private InputAction d_ButtonPressAction;
    private InputAction f_ButtonPressAction;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    float a_ButtonInput;
    float s_ButtonInput;
    float d_ButtonInput;
    float f_ButtonInput;

    public bool canBePressed;

    private void OnEnable()
    {
        inputAction.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputAction.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        var playerMap = inputAction.FindActionMap("Player");
        a_ButtonPressAction = playerMap.FindAction("A");
        s_ButtonPressAction = playerMap.FindAction("S");
        d_ButtonPressAction = playerMap.FindAction("D");
        f_ButtonPressAction = playerMap.FindAction("F");
    }
    void Start()
    {

    }

    void Update()
    {
        GetInput();
        HandleButtonPresses();



    }
    void GetInput()
    {
        s_ButtonInput = s_ButtonPressAction.ReadValue<float>();
        a_ButtonInput = a_ButtonPressAction.ReadValue<float>();
        d_ButtonInput = d_ButtonPressAction.ReadValue<float>();
        f_ButtonInput = f_ButtonPressAction.ReadValue<float>();
    }
    void HandleButtonPresses()
    {
        if (s_ButtonInput > .1f)
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) < 14.75f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 14.20)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 13.85f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 12.10f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                if (Mathf.Abs(transform.position.y) < 12.45f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }

        if (a_ButtonInput > .1f)
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) < 14.75f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 14.20)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 13.85f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 12.10f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                if (Mathf.Abs(transform.position.y) < 12.45f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }

        if (d_ButtonInput > .1f)
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) < 14.75f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 14.20)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 13.85f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 12.10f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                if (Mathf.Abs(transform.position.y) < 12.45f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }

        if (f_ButtonInput > .1f)
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) < 14.75f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 14.20)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 13.85f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 12.10f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                if (Mathf.Abs(transform.position.y) < 12.45f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}
