using UnityEngine;
using System.Collections;

public class NodeSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] public Vector3 prefabSize = new Vector3(0, 2.082438f, 2.082438f);
    public int ChanceOfLongNotes;
    [SerializeField] public Material[] Materials;
    public Coroutine MyRunningCoroutine;
    private int PurpleNoteCounter = 0;
    [SerializeField] public bool DoublePointState = false;
    public int ChanceOfPurple;
    public GameManager gameManager;
    //[SerializeField] private int ChanceOfPurpleOdds = 11;
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        MyRunningCoroutine = StartCoroutine(SpawnRoutine());

    }

    public IEnumerator SpawnRoutine()
    {
        
        while (true)
        {
            ChanceOfLongNotes = Random.Range(0, 11);
            int i = Random.Range(0, prefabs.Length);
            /*for (int i = 0; i < prefabs.Length; i++)
            {*/
            ChanceOfPurple = Random.Range(0, /*ChanceOfPurple*/ 11);
                
            //}

            if (ChanceOfLongNotes == 10)
            {
                prefabSize.x = Random.Range(6, 10f);
                prefabs[i].transform.localScale = prefabSize;
                prefabs[i].gameObject.tag = "LongNote";
                
                if (DoublePointState)
                {
                    prefabs[i].GetComponent<Renderer>().material = Materials[4];

                }
                else
                {
                    prefabs[i].GetComponent<Renderer>().material = Materials[i];
                }
                Instantiate(prefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
                yield return new WaitForSeconds(spawnInterval);
                
                //ChanceOfLongNotes = Random.Range(0, 6);
            }
            else
            {
                prefabSize.x = 2.082438f;
                prefabs[i].transform.localScale = prefabSize;
                prefabs[i].gameObject.tag = "ShortNote";
                if (PurpleNoteCounter < 5)
                {   
                    if ((ChanceOfPurple == 1))
                    {
                        prefabs[i].GetComponent<Renderer>().material = Materials[4];
                        PurpleNoteCounter++;
                    }
                    /*if ((gameManager.MusicTimeLeft == 0.8 * gameManager.music.length) && (PurpleNoteCounter < 2))
                    {
                        ChanceOfPurpleOdds = 1;
                    }
                    if ((gameManager.MusicTimeLeft == 0.4 * gameManager.music.length) && (PurpleNoteCounter < 4))
                    {
                        ChanceOfPurpleOdds = 1;
                    }*/        
                    else
                    {
                        prefabs[i].GetComponent<Renderer>().material = Materials[i];
                    }

                }
                else if (DoublePointState)
                {
                    prefabs[i].GetComponent<Renderer>().material = Materials[4];
                }
                
                Instantiate(prefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
                yield return new WaitForSeconds(spawnInterval);
                
                //ChanceOfLongNotes = Random.Range(0, 6);
            }
            
        }

    }
    public void StopSpawning()
    {
        if (MyRunningCoroutine != null)
        {
            StopCoroutine(MyRunningCoroutine);
            MyRunningCoroutine = null;
            Debug.Log("SpawnRoutine stopped.");
        }
    }

    public void StartSpawning()
    {
        if (MyRunningCoroutine == null)
        {
            MyRunningCoroutine = StartCoroutine(SpawnRoutine());
            Debug.Log("SpawnRoutine started.");
            PurpleNoteCounter = 0;
        }
    }
}
