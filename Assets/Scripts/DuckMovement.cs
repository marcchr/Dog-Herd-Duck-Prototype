using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        float force = 1f;
        if (collision.gameObject.tag == "Dog")
        {
            Vector3 direction = collision.transform.position - transform.position;

            direction = -direction.normalized;

            gameObject.GetComponent<Rigidbody>().AddForce(direction * force);
        }
    }

    private void Update()
    {
    }


}
