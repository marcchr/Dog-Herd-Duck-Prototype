using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Vector3 targetPosition;
    [SerializeField] float moveSpeed;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] float amplitudeOffset;
    [SerializeField] float duration = 5f;
    private float timer;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = player.transform.position;
        trailRenderer = GetComponent<TrailRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //float percentage = timer / duration;

        //var yOffset = new Vector3(0, Mathf.Sin(timer * moveSpeed) * amplitude + amplitudeOffset, 0);
        //transform.position = Vector3.MoveTowards(transform.position + yOffset, targetPosition + yOffset, moveSpeed * timer);

        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        direction.y += Mathf.Sin(Time.time * frequency) * amplitude;

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //if (Mathf.Abs(targetPosition.y - transform.position.y) < Mathf.Abs(targetPosition.x - transform.position.x))
        //{
        //    var yOffset = new Vector3(0, Mathf.Sin(timer * moveSpeed) * amplitude + amplitudeOffset, 0);
        //    transform.position = Vector3.Lerp(transform.position + yOffset, targetPosition + yOffset, percentage);
        //}
        //else
        //{
        //    var xOffset = new Vector3(Mathf.Sin(timer * moveSpeed) * amplitude + amplitudeOffset, 0, 0);
        //    transform.position = Vector3.Lerp(transform.position + xOffset, targetPosition + xOffset, percentage);
        //}
        // Mathf.Sin(Time.time * moveSpeed) * amplitude + amplitudeOffset);
    }
}
