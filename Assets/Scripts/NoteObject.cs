using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;

    private InputAction upButtonPressAction;
    private InputAction downButtonPressAction;
    private InputAction leftButtonPressAction;
    private InputAction rightButtonPressAction;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    float upButtonInput;
    float leftButtonInput;
    float rightButtonInput;
    float downButtonInput;

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
        upButtonPressAction = playerMap.FindAction("UpButton");
        leftButtonPressAction = playerMap.FindAction("LeftButton");
        rightButtonPressAction = playerMap.FindAction("RightButton");
        downButtonPressAction = playerMap.FindAction("DownButton");
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
        upButtonInput = upButtonPressAction.ReadValue<float>();
        leftButtonInput = leftButtonPressAction.ReadValue<float>();
        rightButtonInput = rightButtonPressAction.ReadValue<float>();
        downButtonInput = downButtonPressAction.ReadValue<float>();
    }
    void HandleButtonPresses()
    {
        if (upButtonInput > .1f)
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

        if (leftButtonInput > .1f)
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

        if (rightButtonInput > .1f)
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

        if (downButtonInput > .1f)
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
