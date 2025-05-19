using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue1 : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogue;
    private int index;

    [SerializeField] private GameObject continueButton;
    public float wordSpeed;
    public bool playerIsClose;

    private CircleCollider2D circleCollider2D;
    private bool firstActivation;
    public GameObject fox;

    private void Start()
    {
        dialogueText.text = "";
        firstActivation = false;
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q) && playerIsClose) || playerIsClose && firstActivation == false)
        {
            firstActivation = true;
            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        circleCollider2D.radius = 2f;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
            fox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            //ZeroText();
        }
    }
}
