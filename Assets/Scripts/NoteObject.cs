using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    private GameControls controls;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public bool canBePressed;
    public GameManager manager = null;

    public KeyCode laneKey; // <-- Assign in Inspector: A, S, D, F

    private void Awake()
    {
        controls = new GameControls();
    }

    private void OnEnable()
    {
        controls.PlayerInput.Enable();

        // Bind each lane key separately
        controls.PlayerInput.HitA.performed += ctx => TryHitKey(KeyCode.A);
        controls.PlayerInput.HitS.performed += ctx => TryHitKey(KeyCode.S);
        controls.PlayerInput.HitD.performed += ctx => TryHitKey(KeyCode.D);
        controls.PlayerInput.HitF.performed += ctx => TryHitKey(KeyCode.F);

        controls.PlayerInput.HitA.canceled += ctx => TryHitKey(KeyCode.A);
        controls.PlayerInput.HitS.canceled += ctx => TryHitKey(KeyCode.S);
        controls.PlayerInput.HitD.canceled += ctx => TryHitKey(KeyCode.D);
        controls.PlayerInput.HitF.canceled += ctx => TryHitKey(KeyCode.F);
    }

    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }

    void TryHitKey(KeyCode pressedKey)
    {
        if (!canBePressed) return;

        //// Only hit if the key matches this note's lane
        //if (pressedKey != laneKey) return;

        //if (tag == "ShortNote" || tag == "LongNote")
        //    gameObject.SetActive(false);

        if (GetComponent<Renderer>().material.color == Color.purple)
            manager.PurpleNoteHitCounter++;

        CheckScore();
    }

    void CheckScore()
    {

        if (transform.position.x <= 16.7f && transform.position.x >= 14.7f)
        {
            Debug.Log("Hit");
            GameManager.instance.NormalHit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (transform.position.x <= 14.8f && transform.position.x >= 13.64f)
        {
            Debug.Log("Good");
            GameManager.instance.GoodHit();
            Instantiate(goodEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (transform.position.x <= 13.65f && transform.position.x >= 12.7f)
        {
            Debug.Log("Perfect");
            GameManager.instance.PerfectHit();
            Instantiate(perfectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (transform.position.x <= 12.8f && transform.position.x >= 11.4f)
        {
            Debug.Log("Hit");
            GameManager.instance.NormalHit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (transform.position.x < 11.5f)
        {
            MissNote();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
            canBePressed = true;
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
        GameManager.instance.NoteMissed();
        Instantiate(missEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
