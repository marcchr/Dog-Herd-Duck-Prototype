using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager2 : MonoBehaviour
{
    public GameObject[] popUps;
    [SerializeField] private int popUpIndex;
    [SerializeField] private GameObject dogFOV;
    [SerializeField] private GameObject[] feathers;
    [SerializeField] private GameObject upperLeftUI;
    [SerializeField] private GameObject featherCounterUI;
    public float waitTime = 3f;

    // [SerializeField] private GameObject fox;

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                upperLeftUI.SetActive(true);
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                featherCounterUI.SetActive(true);
                LevelManager.Instance.AddFeather();
                LevelManager.Instance.AddFeather();
                LevelManager.Instance.AddFeather();
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.E) && LevelManager.Instance.feathers == 2)
            {
                for (int j = 0; j < feathers.Length; j++)
                {
                    feathers[j].SetActive(true);
                }
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (waitTime <= 0f)
            {
                LevelManager.Instance.hasCountdown = true;
                dogFOV.SetActive(true);
                popUps[3].SetActive(false);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
