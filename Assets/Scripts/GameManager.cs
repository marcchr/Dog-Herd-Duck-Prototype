using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject spawner;
    public TextMeshProUGUI herdedNumText;
    public TextMeshProUGUI levelCompleteText;
    public int herdedNum = 0;
    public int herdTotal;

    // Start is called before the first frame update
    void Start()
    {
        herdTotal = spawner.GetComponent<DuckSpawner>().amountToSpawn;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();
    }

    public void AddToHerd()
    {
        herdedNum++;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();

    }

    public void SubtractFromHerd()
    {
        herdedNum--;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();

    }

    

    // Update is called once per frame
    void Update()
    {
        herdTotal = spawner.GetComponent<DuckSpawner>().amountToSpawn;

        if (herdedNum == herdTotal)
        {
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        levelCompleteText.gameObject.SetActive(true);
    }
}
