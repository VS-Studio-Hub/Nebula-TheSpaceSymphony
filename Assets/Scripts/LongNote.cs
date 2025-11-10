using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LongNote : MonoBehaviour
{
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public bool canBePressed, laneOne, laneTwo, laneThree, laneFour;


    public InputActionAsset InputActions;

    private InputAction hitAAction;
    private InputAction hitSAction;
    private InputAction hitDAction;
    private InputAction hitFAction;


    private void OnEnable()
    {
        InputActions.FindActionMap("PlayerInput");
    }

    //private void OnDisable()
    //{
    //    InputActions.FindActionMap("PlayerInput").Disable();
    //}


    private void Awake()
    {
        hitAAction = InputSystem.actions.FindAction("HitA");
        hitSAction = InputSystem.actions.FindAction("HitS");
        hitDAction = InputSystem.actions.FindAction("HitD");
        hitFAction = InputSystem.actions.FindAction("HitF");
    }


    private void Update()
    {
        if (!canBePressed) return;

        if (hitAAction.IsPressed() && laneOne)
            CheckScore();
        if (hitSAction.IsPressed() && laneTwo)
            CheckScore();
        if(hitDAction.IsPressed() && laneThree)
            CheckScore();
        if(hitFAction.IsPressed() && laneFour)
            CheckScore();
    }

    void CheckScore()
    {
        if (transform.position.x <= 15.1f && transform.position.x >= 14.6f)
        {
            Debug.Log("Hit");
            GameManager.instance.LargeNoteHitValue();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        else if (transform.position.x <= 14.5f && transform.position.x >= 13.6f)
        {
            Debug.Log("Good");
            GameManager.instance.LargeNoteHitValue();
            Instantiate(goodEffect, transform.position, Quaternion.identity);
        }
        else if (transform.position.x <= 13.5f && transform.position.x >= 12.6f)
        {
            Debug.Log("Perfect");
            GameManager.instance.LargeNoteHitValue();
            Instantiate(perfectEffect, transform.position, Quaternion.identity);
        }
        else if (transform.position.x < 12.6f)
        {
            Debug.Log("Hit");
            GameManager.instance.LargeNoteHitValue();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
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