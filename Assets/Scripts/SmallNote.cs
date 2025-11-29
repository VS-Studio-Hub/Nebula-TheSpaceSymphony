using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmallNote : MonoBehaviour
{
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public bool canBePressed, laneOne, laneTwo, laneThree, laneFour;

    public InputActionAsset InputActions;

    private InputAction hitAAction;
    private InputAction hitSAction;
    private InputAction hitDAction;
    private InputAction hitFAction;

    private Renderer rend;
    public Material[] defaultMaterial;

    private AudioSource audioSource;


    private void OnEnable()
    {
        InputActions.FindActionMap("PlayerInput");
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        hitAAction = InputSystem.actions.FindAction("HitA");
        hitSAction = InputSystem.actions.FindAction("HitS");
        hitDAction = InputSystem.actions.FindAction("HitD");
        hitFAction = InputSystem.actions.FindAction("HitF");
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if(laneOne) 
            rend.material = defaultMaterial[0];
        if(laneTwo)
            rend.material = defaultMaterial[1];
        if(laneThree)
            rend.material = defaultMaterial[2];
        if(laneFour)
            rend.material = defaultMaterial[3];
    }

    private void Update()
    {
        if (GameManager.instance.activatePurpleNote)
        {
            rend.material = MaterialManager.instance.GetCurrentMaterial();
        }
        else
        {
            if (laneOne)
                rend.material = defaultMaterial[0];
            if (laneTwo)
                rend.material = defaultMaterial[1];
            if (laneThree)
                rend.material = defaultMaterial[2];
            if (laneFour)
                rend.material = defaultMaterial[3];
        }

        if (!canBePressed) return;
        if (!SPButtonController.instance.Swapped())
        {

        if (hitAAction.WasPressedThisFrame() && laneOne)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        if (hitSAction.WasPressedThisFrame() && laneTwo)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        if (hitDAction.WasPressedThisFrame() && laneThree)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        if (hitFAction.WasPressedThisFrame() && laneFour)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        }
        else
        {

            if (hitAAction.WasPressedThisFrame() && laneFour)
            {
                Debug.Log("Hit");

                CheckScore();
            }
            if (hitSAction.WasPressedThisFrame() && laneThree)
            {
                Debug.Log("Hit");

                CheckScore();
            }
            if (hitDAction.WasPressedThisFrame() && laneTwo)
            {
                Debug.Log("Hit");

                CheckScore();
            }
            if (hitFAction.WasPressedThisFrame() && laneOne)
            {
                Debug.Log("Hit");

                CheckScore();
            }
        }

    }

    void CheckScore()
    {
        audioSource.Play();
        if (transform.position.x <= 16.7f && transform.position.x >= 14.7f)
        {
            Debug.Log("Hit");
            GameManager.instance.SmallNoteHit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
        else if (transform.position.x <= 14.8f && transform.position.x >= 13.64f)
        {
            Debug.Log("Good");
            GameManager.instance.SmallNoteGood();
            Instantiate(goodEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
        else if (transform.position.x <= 13.65f && transform.position.x >= 12.7f)
        {
            Debug.Log("Perfect");
            GameManager.instance.SmallNotePerfect();
            Instantiate(perfectEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
        else if (transform.position.x <= 12.8f && transform.position.x >= 11.4f)
        {
            Debug.Log("Hit");
            GameManager.instance.SmallNoteHit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
            canBePressed = true;

        if (other.CompareTag("LaneOne"))
            laneOne = true;
        if (other.CompareTag("LaneTwo"))
            laneTwo = true;
        if (other.CompareTag("LaneThree"))
            laneThree = true;
        if (other.CompareTag("LaneFour"))
            laneFour = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Activator") && gameObject.activeSelf)
        {
            canBePressed = false;
            MissNote();
        }
    }

    void MissNote()
    {
        //GameManager.instance.NoteMissed();
        Instantiate(missEffect, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }
}
