using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject[] pipe;
    public GameObject[] coin;
    public GameManager gameManager;

    public float spawnRate = 4f;
    //private float timer = 0f;
    public float heightOffset = 10f;

    private List<GameObject> spawnedPipes = new List<GameObject>();

    void Start()
    {
        //SpawmnPipe();

        InvokeRepeating("SpawmnPipe", 0, spawnRate);
        InvokeRepeating("SpawmnCoin", 0, spawnRate);
    }

    //void Update()
    //{
    //    spawnedPipes.RemoveAll(pipe => pipe == null);

    //    if (timer < spawnRate)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        SpawmnPipe();
    //        SpawmnCoin();
    //        timer = 0f;
    //    }
    //}

    void SpawmnPipe()
    {
        if (pipe == null)
        {
            Debug.LogError("Pipe prefab is not assigned!");
            return;
        }

        float lowPoint = transform.position.y - heightOffset;
        float highPoint = transform.position.y + heightOffset;

        GameObject newPipe = Instantiate(pipe[GameSwitcher.Instance.currentPhase], new Vector3(transform.position.x, Random.Range(lowPoint, highPoint), 0), transform.rotation);
        spawnedPipes.Add(newPipe);

       // if (gameManager != null)
        //{
        //    gameManager.PassPipe();
       // }
       // else
       // {
         //   Debug.LogError("GameManager reference is not assigned!");
       // }
    }

    void SpawmnCoin()
    {
        if (spawnedPipes.Count < 2) return;

        GameObject upperPipe = spawnedPipes[spawnedPipes.Count - 1];
        GameObject lowerPipe = spawnedPipes[spawnedPipes.Count - 2];

        if (upperPipe == null || lowerPipe == null)
        {
            Debug.LogWarning("One of the pipes in the list is null. Cleaning up...");
            spawnedPipes.RemoveAll(pipe => pipe == null);
            return;
        }

        float upperPipeHeight = upperPipe.transform.position.y;
        float lowerPipeHeight = lowerPipe.transform.position.y;

        float coinHeight = Random.Range(Mathf.Min(upperPipeHeight, lowerPipeHeight) + 1f, Mathf.Max(upperPipeHeight, lowerPipeHeight) - 3f);

        GameObject newCoin = Instantiate(coin[GameSwitcher.Instance.currentPhase], new Vector3(transform.position.x, coinHeight, 0), transform.rotation);

        if (newCoin.GetComponent<CoinMove>() == null)
        {
            newCoin.AddComponent<CoinMove>();
        }

        // Remove this line:
        // if (gameManager != null) { gameManager.CollectCoin(); }
    }



    bool IsPositionOccupiedByPipe(Vector3 position)
    {
        for (int i = spawnedPipes.Count - 1; i >= 0; i--)
        {
            if (spawnedPipes[i] == null)
            {
                spawnedPipes.RemoveAt(i);
                continue;
            }

            if (Vector3.Distance(spawnedPipes[i].transform.position, position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }
}