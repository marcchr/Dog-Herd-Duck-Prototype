using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdArea : MonoBehaviour
{
    public ParticlesManager particlesManager;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DuckMovement>() != null)
        {
            //collision.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            //collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameManager.AddToHerd();

            GameObject hearts = particlesManager.GetPooledObject();
            if (hearts != null)
            {
                hearts.transform.position = collision.transform.position;
                hearts.SetActive(true);

            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DuckMovement>() != null)
        {
            gameManager.SubtractFromHerd();
        }
    }
}
