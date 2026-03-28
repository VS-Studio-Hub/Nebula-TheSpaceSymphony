using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnManager : MonoBehaviour
{
    [Header("Timings")]
    public float[] smallNoteTimings;
    public List<Vector2> longNoteSize; // x = startTime, y = endTime

    [Header("References")]
    public GameObject smallNotePrefab;
    public GameObject longNotePrefab;
    public GameObject purpleNote;
    public GameObject noteSpawnVfx;
    public Transform spawnPoint;

    [Header("Audio Clips")]
    public AudioClip[] smnClip;
    public AudioClip[] lgnClip;

    [Header("Settings")]
    public float noteTravelTime = 2.0f;

    private int currentSmallNoteIndex = 0;
    private int currentLongNoteIndex = 0;
    private int smnIndex = 0;
    private int lgnIndex = 0;

    private bool purple = false;

    void Start()
    {
        currentSmallNoteIndex = 0;
        currentLongNoteIndex = 0;
        smnIndex = 0;
        lgnIndex = 0;
        noteSpawnVfx.gameObject.SetActive(false);

        //if (GameManager.instance != null)
        //{
        //    GameManager.instance.StartMusic();
        //}
    }

    void Update()
    {
        if (GameManager.instance == null || GameManager.instance.musicSource == null || GameManager.gameOver)
            return;

        float songTime = GameManager.instance.musicSource.time;

        // Small notes
        while (currentSmallNoteIndex < smallNoteTimings.Length &&
               songTime >= smallNoteTimings[currentSmallNoteIndex] - noteTravelTime)
        {
            SpawnSmallNote();
            currentSmallNoteIndex++;
        }

        // Long notes
        while (currentLongNoteIndex < longNoteSize.Count &&
               songTime >= longNoteSize[currentLongNoteIndex].x - noteTravelTime)
        {
            float startTime = longNoteSize[currentLongNoteIndex].x;
            float endTime = longNoteSize[currentLongNoteIndex].y;

            SpawnLongNote(startTime, endTime);
            currentLongNoteIndex++;
        }
    }

    private void SpawnSmallNote()
    {
        bool isPurple = Random.Range(0, 9) == 0; // 1 in 9 chance

        GameObject prefabToSpawn = isPurple ? purpleNote : smallNotePrefab;
        GameObject note = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        StartCoroutine(notesSpawnVfx());

        AudioSource audioSource = note.GetComponent<AudioSource>();
        if (audioSource != null && smnIndex < smnClip.Length)
        {
            audioSource.clip = smnClip[smnIndex];
        }

        smnIndex++;
    }

    IEnumerator notesSpawnVfx()
    {
        noteSpawnVfx.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        noteSpawnVfx.gameObject.SetActive(false);
    }

    private void SpawnLongNote(float startTime, float endTime)
    {
        GameObject longNote = Instantiate(longNotePrefab, spawnPoint.position, Quaternion.identity);

        float noteLength = Mathf.Max(0.1f, endTime - startTime);

        Vector3 scale = longNote.transform.localScale;
        scale.x = noteLength;
        longNote.transform.localScale = scale;

        AudioSource audioSource = longNote.GetComponent<AudioSource>();
        if (audioSource != null && lgnIndex < lgnClip.Length)
        {
            audioSource.clip = lgnClip[lgnIndex];
        }

        lgnIndex++;
    }
}