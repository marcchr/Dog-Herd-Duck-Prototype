using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    [SerializeField] private int popUpIndex;
    [SerializeField] private GameObject dogFOV;
    [SerializeField] private GameObject upperLeftUI;
    public float waitTime = 3f;


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
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
            {
                dogFOV.SetActive(true);
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
            {
                popUpIndex++;
                //set active foxes
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                upperLeftUI.SetActive(true);
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if (waitTime <= 0)
            {
                popUps[4].SetActive(false);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
