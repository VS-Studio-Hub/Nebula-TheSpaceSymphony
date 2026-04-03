using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class SmallNote : MonoBehaviour
{
    public GameObject mesh;

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
    /*public CrackingScreenController CrackController;
    public int Counter = 0;*/
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
        /*if (CrackController == null)
        {
            CrackController = GameObject.Find("Custom Pass").GetComponent<CrackingScreenController>();
        }*/
    }



    private void Update()
    {

        if (!canBePressed && !pressed) return;


        if (hitAAction.WasPressedThisFrame() && !activatedNote && laneOne)
        {
            CheckScore();
        }
        if (hitSAction.WasPressedThisFrame() && !activatedNote && laneTwo)
        {
            CheckScore();
        }
        if (hitDAction.WasPressedThisFrame() && !activatedNote && laneThree)
        {
            CheckScore();
        }
        if (hitFAction.WasPressedThisFrame() && !activatedNote && laneFour)
        {
            CheckScore();
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
            mesh.SetActive(false);
            activatedNote = true;
            VFX();
            GameManager.instance.ResetEmptyPressCount();
        }
        else if (transform.position.x <= 8.5f && transform.position.x >= 6f)
        {
            audioSource.Play();
            Debug.Log("Good");
            GameManager.instance.SmallNoteGood();
            UIManager.instance.Hit();
            mesh.SetActive(false);
            activatedNote = true;
            VFX();
            GameManager.instance.ResetEmptyPressCount();
        }
        else if (transform.position.x <= 5.9f && transform.position.x >= 4.4f)
        {
            audioSource.Play();
            Debug.Log("Perfect");
            GameManager.instance.SmallNotePerfect();
            UIManager.instance.Hit();
            mesh.SetActive(false);
            activatedNote = true;
            VFX();
            GameManager.instance.ResetEmptyPressCount();
        }
        else if (transform.position.x <= 4.3f && transform.position.x >= 1f)
        {
            audioSource.Play();
            Debug.Log("Hit");
            GameManager.instance.SmallNoteHit();
            UIManager.instance.Hit();
            mesh.SetActive(false);
            activatedNote = true;
            VFX();
            GameManager.instance.ResetEmptyPressCount();
        }

    }

    private void VFX()
    {
        if (laneOne)
        {
            if (GameManager.instance.activatePurpleNote)
                VFXManager.greenSpark = true;
            else
                VFXManager.spark = true;

            VFXManager.leftSoundWave = true;

            VFXManager.instance.LaneOneVFX();
        }
        if (laneTwo)
        {
            if (GameManager.instance.activatePurpleNote)
                VFXManager.greenSpark = true;
            else
                VFXManager.spark = true;

            VFXManager.leftSoundWave = true;

            VFXManager.instance.LaneTwoVFX();
        }
        if (laneThree)
        {
            if (GameManager.instance.activatePurpleNote)
                VFXManager.greenSpark = true;
            else
                VFXManager.spark = true;

            VFXManager.rightSoundWave = true;

            VFXManager.instance.LaneThreeVFX();
        }
        if (laneFour)
        {
            if (GameManager.instance.activatePurpleNote)
                VFXManager.greenSpark = true;
            else
                VFXManager.spark = true;

            VFXManager.rightSoundWave = true;

            VFXManager.instance.LaneFourVFX();
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
        GameManager.instance.MissNotesValue();
        UIManager.instance.Miss();
        mesh.SetActive(false);
        if (laneOne)
        {
            VFXManager.blackHole = true;
            VFXManager.instance.LaneOneVFX();
        }
        if (laneTwo)
        {
            VFXManager.blackHole = true;
            VFXManager.instance.LaneTwoVFX();
        }
        if (laneThree)
        {
            VFXManager.blackHole = true;
            VFXManager.instance.LaneThreeVFX();
        }
        if (laneFour)
        {
            VFXManager.blackHole = true;
            VFXManager.instance.LaneFourVFX();
        }
        
        /*if (Counter == 0)
        {
            CrackController.enabled = true;
        }
        else
        {
            CrackController.EffectVisibility();
        }*/
    }
}