using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartParticles : MonoBehaviour
{
    float delay = 0f;
    bool coroutineStarted;
    // Start is called before the first frame update
    void Start()
    {
        delay = GetComponent<ParticleSystem>().main.duration;
        coroutineStarted = true;
        StartCoroutine(SetActiveFalse());
        Debug.Log("start coroutine");
    }

    IEnumerator SetActiveFalse()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        coroutineStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true && coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(SetActiveFalse());
        }
    }
}
