using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public int amountToSpawn;
    public GameObject duckPrefab;

    [SerializeField] Transform[] hidingSpots;

    void Start()
    {
        for (int i = 0; i < amountToSpawn - hidingSpots.Length; i++)
        {
            var spawnPoint = new Vector3(Random.Range(-22, 22), Random.Range(-12, 12), 0);
            if ((spawnPoint-transform.position).magnitude < 10)
            {
                i--;
                continue;
            }
            else
            {
                var duck = Instantiate(duckPrefab, spawnPoint, Quaternion.identity);
                //duck.gameObject.GetComponentInChildren<Animator>().SetTrigger("spawnDuck");
            }
        }

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            var spawnPoint = new Vector3(hidingSpots[i].position.x, hidingSpots[i].position.y + 2f);
            var duck = Instantiate(duckPrefab, spawnPoint, Quaternion.identity);
            duck.GetComponent<DuckMovement>().isHiding = true;

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
