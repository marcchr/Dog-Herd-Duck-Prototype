using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public int amountToSpawn;
    public GameObject duckPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            var spawnPoint = new Vector3(Random.Range(-22, 22), Random.Range(-12, 12), 0);
            if ((spawnPoint-transform.position).magnitude < 10)
            {
                i--;
                continue;
            }
            else
            {
                Instantiate(duckPrefab, spawnPoint, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
