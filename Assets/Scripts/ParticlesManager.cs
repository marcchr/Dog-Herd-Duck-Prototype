using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlesManager : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject heartParticlesPrefab;
    public int amountToPool;


    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(heartParticlesPrefab, transform);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
