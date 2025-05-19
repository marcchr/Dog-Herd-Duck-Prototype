using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject spawner;
    public TextMeshProUGUI herdedNumText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI levelFailedText;
    public int herdedNum = 0;
    public int herdTotal;

    [SerializeField] Sprite heartSprite;
    [SerializeField] Sprite skullSprite;
    [SerializeField] Image[] livesDisplay;
    public int lives = 3;

    public TextMeshProUGUI feathersNumText;
    public int feathers = 0;

    [SerializeField] GameObject reloadButton;

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

    public void AddFeather()
    {
        feathers++;
        feathersNumText.text = feathers.ToString() + "x";
    }
    public void UseFeather()
    {
        feathers--;
        feathersNumText.text = feathers.ToString() + "x";
    }



    // Update is called once per frame
    void Update()
    {
        herdTotal = spawner.GetComponent<DuckSpawner>().amountToSpawn;

        if (herdedNum == herdTotal)
        {
            herdedNum = herdTotal;
            LevelComplete();
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void LevelComplete()
    {
        levelCompleteText.gameObject.SetActive(true);
    }

    public void LoseLife()
    {
        lives--;
        livesDisplay[lives].sprite = skullSprite;
    }

    public void GameOver()
    {
        levelFailedText.gameObject.SetActive(true);
        reloadButton.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Reload()
    {
        //Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
