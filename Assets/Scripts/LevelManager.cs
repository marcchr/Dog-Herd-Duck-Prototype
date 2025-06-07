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

    public GameObject levelCompletePanel;
    public TextMeshProUGUI levelResultText;
    public GameObject nextLevelButton;

    public int herdedNum = 0;
    public int herdTotal;

    [SerializeField] Sprite heartSprite;
    [SerializeField] Sprite skullSprite;
    [SerializeField] Image[] livesDisplay;
    [SerializeField] GameObject[] performanceDisplay;
    public int lives = 3;

    public TextMeshProUGUI feathersNumText;
    public int feathers = 0;

    private float levelTimer = 0f;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] private GameObject inGamePanel;

    public bool hasCountdown;
    [SerializeField] SpriteRenderer backgroundSprite;
    public Color targetColor;
    private Color startingColor;
    public float transitionDuration;

    private float dayTimer = 0f;
    [SerializeField] Image dayTimerFill;

    // Start is called before the first frame update
    void Start()
    {
        herdTotal = spawner.GetComponent<DuckSpawner>().amountToSpawn;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();

        inGamePanel.SetActive(true);

        if(backgroundSprite != null )
        {
            startingColor = backgroundSprite.color;
        }
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
        UpdateTimer();

        if (herdedNum == herdTotal)
        {
            herdedNum = herdTotal;
            LevelComplete();
            PlayerPrefs.SetInt("UnlockedLevel", 2);
        }

        if (lives <= 0)
        {
            GameOver();
        }

        if (hasCountdown)
        {
            dayTimer += Time.deltaTime;
            float normalizedTime = dayTimer / transitionDuration;

            normalizedTime = Mathf.Clamp01(normalizedTime);

            backgroundSprite.color = Color.Lerp(startingColor, targetColor, normalizedTime);
            dayTimerFill.fillAmount = 1 - normalizedTime;

            if (normalizedTime > 0.5f && normalizedTime <= 0.75f)
            {
                performanceDisplay[2].SetActive(false);
            }
            if (normalizedTime > 0.75f && normalizedTime <= 1f)
            {
                performanceDisplay[1].SetActive(false);
            }


            if (herdedNum == herdTotal)
            {
                herdedNum = herdTotal;
           
                LevelComplete();
            }

            if (dayTimer >= transitionDuration)
            {
                performanceDisplay[0].SetActive(false);
                GameOver();
            }
        }

    }

    public void LevelComplete()
    {
        backgroundSprite.gameObject.SetActive(false);
        inGamePanel.SetActive(false);
        levelCompletePanel.gameObject.SetActive(true); //replace with levelCompletePanel
        levelResultText.text = "Level Complete!";
        Time.timeScale = 0f;

    }

    public void LoseLife()
    {
        lives--;
        livesDisplay[lives].sprite = skullSprite;
        herdTotal--;
        herdedNumText.text = "Ducks herded: " + herdedNum.ToString() + "/" + herdTotal.ToString();

        performanceDisplay[lives].SetActive(false);
    }

    public void GameOver()
    {
        backgroundSprite.gameObject.SetActive(false);
        inGamePanel.SetActive(false);
        levelCompletePanel.gameObject.SetActive(true);
        levelResultText.text = "Level Failed...";
        nextLevelButton.SetActive(false);
        Time.timeScale = 0f;
    }

    void UpdateTimer()
    {
        levelTimer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(levelTimer / 60);
        int seconds = Mathf.FloorToInt(levelTimer % 60);
        timerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
