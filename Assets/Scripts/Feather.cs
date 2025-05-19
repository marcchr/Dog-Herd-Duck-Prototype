using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision feather detected");
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.AddFeather();
            this.gameObject.SetActive(false);
        }
    }
}
