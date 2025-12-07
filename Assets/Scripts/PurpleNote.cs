using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class PurpleNote : MonoBehaviour
{
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect,mesh;
    public bool canBePressed, laneOne, laneTwo, laneThree, laneFour;
    public bool activatedNote = false;
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

    private void Start()
    {

    }

    private void Update()
    {

        if (!canBePressed) return;

        if (hitAAction.WasPressedThisFrame() && laneOne && !activatedNote)
        {
            CheckScore();

        }
        if (hitSAction.WasPressedThisFrame() && laneTwo && !activatedNote)
        {
            CheckScore();

        }
        if (hitDAction.WasPressedThisFrame() && laneThree && !activatedNote)
        {
            CheckScore();

        }
        if (hitFAction.WasPressedThisFrame() && laneFour && !activatedNote)
        {
            CheckScore();

        }
    }

    void CheckScore()
    {
        if (transform.position.x <= 8.6f && transform.position.x >= 7.9f)
        {
            audioSource.Play();
            Debug.Log("Hit");
            GameManager.instance.SmallNoteHit();
            UIManager.instance.Hit();
            if(!GameManager.instance.activatePurpleNote)
                UIManager.instance.BoostHit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (GameManager.instance.activatePurpleNote)
                GameManager.instance.PurpleActivatedNoteValue();
            else
                GameManager.instance.PurpleNoteValue();
            mesh.SetActive(false);
        }
        else if (transform.position.x <= 7.8f && transform.position.x >= 6f)
        {
            audioSource.Play();
            GameManager.instance.SmallNoteGood();
            UIManager.instance.Hit();
            if (!GameManager.instance.activatePurpleNote)
                UIManager.instance.BoostHit();
            Instantiate(goodEffect, transform.position, Quaternion.identity);
            if (GameManager.instance.activatePurpleNote)
                GameManager.instance.PurpleActivatedNoteValue();
            else
                GameManager.instance.PurpleNoteValue();
            mesh.SetActive(false);
        }
        else if (transform.position.x <= 5.9f && transform.position.x >= 4.4f)
        {
            audioSource.Play();
            GameManager.instance.SmallNotePerfect();
            UIManager.instance.Hit();
            if (!GameManager.instance.activatePurpleNote)
                UIManager.instance.BoostHit();
            Instantiate(perfectEffect, transform.position, Quaternion.identity);
            if (GameManager.instance.activatePurpleNote)
                GameManager.instance.PurpleActivatedNoteValue();
            else
                GameManager.instance.PurpleNoteValue();
            mesh.SetActive(false);
        }
        else if (transform.position.x <= 4.3f && transform.position.x >= 1f)
        {
            audioSource.Play();
            GameManager.instance.SmallNoteHit();
            UIManager.instance.Hit();
            if (!GameManager.instance.activatePurpleNote)
                UIManager.instance.BoostHit();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (GameManager.instance.activatePurpleNote)
                GameManager.instance.PurpleActivatedNoteValue();
            else
                GameManager.instance.PurpleNoteValue();
            mesh.SetActive(false);
        }
        activatedNote = true;
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
        if (other.CompareTag("Activator") && mesh.activeSelf)
        {
            canBePressed = false;
            MissNote();
        }
    }

    public void MissNote()
    {
        audioSource.PlayOneShot(missSound);
        Instantiate(missEffect, transform.position, Quaternion.identity);
        GameManager.instance.PurpleNoteMiss();
        UIManager.instance.Miss();
        mesh.SetActive(false);
    }
}