using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmallNote : MonoBehaviour
{
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect, mesh;

    public bool laneOne, laneTwo, laneThree, laneFour;

    public bool canBePressed = false;
    public bool activatedNote = false;
    public bool pressed = true;

    public InputActionAsset InputActions;

    private InputAction hitAAction;
    private InputAction hitSAction;
    private InputAction hitDAction;
    private InputAction hitFAction;

    private AudioSource audioSource;
    public AudioClip missSound;

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



    private void Update()
    {
        
        if (!canBePressed && !pressed) return;
        if (!SPButtonController.instance.Swapped())
        {

        if (hitAAction.WasPressedThisFrame() && !activatedNote && laneOne)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        if (hitSAction.WasPressedThisFrame() && !activatedNote && laneTwo)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        if (hitDAction.WasPressedThisFrame() && !activatedNote && laneThree)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        if (hitFAction.WasPressedThisFrame() && !activatedNote && laneFour)
        {
            Debug.Log("Hit");

            CheckScore();
        }
        }
        else
        {

            if (hitAAction.WasPressedThisFrame() && !activatedNote && laneOne)
            {
                Debug.Log("Hit");

                CheckScore();
            }
            if (hitSAction.WasPressedThisFrame() && !activatedNote && laneTwo)
            {
                Debug.Log("Hit");

                CheckScore();
            }
            if (hitDAction.WasPressedThisFrame() && !activatedNote && laneThree)
            {
                Debug.Log("Hit");

                CheckScore();
            }
            if (hitFAction.WasPressedThisFrame() && !activatedNote && laneFour)
            {
                Debug.Log("Hit");

                CheckScore();
            }
        }

    }

    void CheckScore()
    {
        pressed = false;
        
        if (transform.position.x <= 15.4f && transform.position.x >= 8.6f)
        {
            audioSource.Play();
            Debug.Log("Hit");
            GameManager.instance.SmallNoteHit();
            UIManager.instance.Hit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            mesh.SetActive(false);
            activatedNote = true;
        }
        else if (transform.position.x <= 8.5f && transform.position.x >= 6f)
        {
            audioSource.Play();
            Debug.Log("Good");
            GameManager.instance.SmallNoteGood();
            UIManager.instance.Hit();
            Instantiate(goodEffect, transform.position, Quaternion.identity);
            mesh.SetActive(false);
            activatedNote = true;

        }
        else if (transform.position.x <= 5.9f && transform.position.x >= 4.4f)
        {
            audioSource.Play();
            Debug.Log("Perfect");
            GameManager.instance.SmallNotePerfect();
            UIManager.instance.Hit();
            Instantiate(perfectEffect, transform.position, Quaternion.identity); 
            mesh.SetActive(false);
            activatedNote = true;
        }
        else if (transform.position.x <= 4.3f && transform.position.x >= 1f)
        {
            audioSource.Play();
            Debug.Log("Hit");
            GameManager.instance.SmallNoteHit();
            UIManager.instance.Hit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            mesh.SetActive(false);
            activatedNote = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
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
        if (other.CompareTag("Activator") && mesh.activeSelf)
        {
            canBePressed = false;
            MissNote();
        }
    }

    void MissNote()
    {
        audioSource.PlayOneShot(missSound);
        GameManager.instance.NoteMissed();
        UIManager.instance.Miss();
        Instantiate(missEffect, transform.position, Quaternion.identity);
        mesh.SetActive(false);
    }
}
