using System.Collections.Generic;
using UnityEngine;

public class NodeSpawnManager : MonoBehaviour
{
    [Header("Timings")]
    public float[] smallNoteTimings;
    public List<Vector2> longNoteSize; // x = startTime, y = endTime

    [Header("References")]
    public GameObject smallNotePrefab;
    public GameObject longNotePrefab;
    public GameObject purpleNote;
    public Transform spawnPoint;
 

    [Header("Settings")]
    public float noteTravelTime = 2.0f;

    private int currentSmallNoteIndex = 0;
    private int currentLongNoteIndex = 0;

    private double songStartDspTime; // When the song actually starts

    bool purple = false;

    private int spawnCount; 
    private int rand;


    void Start()
    {
        // Schedule music to start exactly at dsp time (for precision)
        songStartDspTime = AudioSettings.dspTime;
        GameManager.instance.StartMusic(songStartDspTime);
    }

    void Update()
    {
        // Calculate the precise playback time using dspTime
        double songDspTime = AudioSettings.dspTime - songStartDspTime;
        float songTime = (float)songDspTime;

        // --- Small notes ---
        if (currentSmallNoteIndex < smallNoteTimings.Length)
        {
            if (songTime >= smallNoteTimings[currentSmallNoteIndex] - noteTravelTime)
            {
                SpawnSmallNote();
                currentSmallNoteIndex++;
            }
        }

        // --- Long notes ---
        if (currentLongNoteIndex < longNoteSize.Count)
        {
            float startTime = longNoteSize[currentLongNoteIndex].x;
            float endTime = longNoteSize[currentLongNoteIndex].y;

            if (songTime >= startTime - noteTravelTime)
            {
                SpawnLongNote(startTime, endTime);
                currentLongNoteIndex++;
            }
        }
    }

    private void SpawnSmallNote()
    {
        rand = Random.Range(0, 2);

        if(rand == 0)
        {
            purple = true;
        }

        if (purple)
        {
            Instantiate(purpleNote, spawnPoint.position, Quaternion.identity);
            purple = false;
        }
        else
        {
            Instantiate(smallNotePrefab, spawnPoint.position, Quaternion.identity);
        }

        
    }

    private void SpawnLongNote(float startTime, float endTime)
    {
        GameObject longNote = Instantiate(longNotePrefab, spawnPoint.position, Quaternion.identity);

        float noteLength = Mathf.Max(0.1f, endTime - startTime);
        Vector3 scale = longNote.transform.localScale;
        scale.x = noteLength;
        longNote.transform.localScale = scale;
    }
}
