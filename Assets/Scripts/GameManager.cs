using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject herd;
    public TextMeshProUGUI herdedNumText;
    public TextMeshProUGUI levelCompleteText;
    int herdedNum = 0;
    int herdTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        herdTotal = herd.GetComponent<Herd>().startingCount;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();
    }

    public void AddToHerd()
    {
        herdedNum++;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HerdAgent>() != null)
        {
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            AddToHerd();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
