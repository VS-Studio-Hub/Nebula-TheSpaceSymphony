using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LongNote : MonoBehaviour
{
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public bool canBePressed, laneOne, laneTwo, laneThree, laneFour;

    public bool singleScore = true;

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
        rend = GetComponentInChildren<Renderer>();
        if (laneOne)
            rend.material = defaultMaterial[0];
        if (laneTwo)
            rend.material = defaultMaterial[1];
        if (laneThree)
            rend.material = defaultMaterial[2];
        if (laneFour)
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
        if (singleScore)
        {
            if (hitAAction.IsPressed() && laneOne)
            {
                SingleHitValue();
                singleScore = false;
            }

            if (hitSAction.IsPressed() && laneTwo)
            {
                SingleHitValue();
                singleScore = false;
            }

            if (hitDAction.IsPressed() && laneThree)
            {
                SingleHitValue();
                singleScore = false;
            }

            if (hitFAction.IsPressed() && laneFour)
            {
                SingleHitValue();
                singleScore = false;
            }
        }
        else
        {
            if (hitAAction.IsPressed() && laneOne)
            {
                CheckScore();
            }

            if (hitSAction.IsPressed() && laneTwo)
            {
                CheckScore();
            }

            if (hitDAction.IsPressed() && laneThree)
            {
                CheckScore();
            }

            if (hitFAction.IsPressed() && laneFour)
            {
                CheckScore();
            }
        }
    }

    void CheckScore()
    {
        if (transform.position.x <= 15.1f && transform.position.x >= 14.6f)
        {
            Debug.Log("Hit");
            GameManager.instance.LongNoteHitValue();
        }
        else if (transform.position.x <= 14.5f && transform.position.x >= 13.6f)
        {
            Debug.Log("Good");
            GameManager.instance.LongNoteHitValue();
        }
        else if (transform.position.x <= 13.5f && transform.position.x >= 12.6f)
        {
            Debug.Log("Perfect");
            GameManager.instance.LongNoteHitValue();
        }
        else if (transform.position.x < 12.6f)
        {
            Debug.Log("Hit");
            GameManager.instance.LongNoteHitValue();
        }
    }

    void SingleHitValue()
    {
        if (transform.position.x <= 15.1f && transform.position.x >= 14.6f)
        {
            Debug.Log("Hit");
            GameManager.instance.LongNoteHit();
        }
        else if (transform.position.x <= 14.5f && transform.position.x >= 13.6f)
        {
            Debug.Log("Good");
            GameManager.instance.LongNoteGood();
        }
        else if (transform.position.x <= 13.5f && transform.position.x >= 12.6f)
        {
            Debug.Log("Perfect");
            GameManager.instance.LongNotePerfect();
        }
        else if (transform.position.x < 12.6f)
        {
            Debug.Log("Hit");
            GameManager.instance.LongNoteHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
            audioSource.Play();
        }
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
            Destroy(gameObject);
        }
    }

    void MissNote()
    {
        GameManager.instance.NoteMissed();
        Instantiate(missEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}