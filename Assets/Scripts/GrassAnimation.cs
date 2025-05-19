using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnimation : MonoBehaviour
{
    [SerializeField] float randomOffset;
    [SerializeField] private AudioClip grassRustle;


    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        randomOffset = Random.Range(0f, 1f);
        animator.SetFloat("randomOffset", randomOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("isWalkedOn");
        SoundFXManager.Instance.PlaySoundFXClip(grassRustle, transform, 1f);
    }
}
