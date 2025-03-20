using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : Singleton<SoundFXManager>
{
    [SerializeField] private AudioSource soundFXObject;

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign audioClip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //assign random pitch
        audioSource.pitch = Random.Range(0.8f, 1.2f);

        //play sound
        audioSource.Play();

        //get length of sfx clip
        float clipLength = audioSource.clip.length;

        //destroy clip after done playing
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //assign random index
        int rand = Random.Range(0, audioClip.Length);

        //spawn gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign audioClip
        audioSource.clip = audioClip[rand];

        //assign volume
        audioSource.volume = volume;

        //assign random pitch
        audioSource.pitch = Random.Range(0.8f, 1.2f);

        //play sound
        audioSource.Play();

        //get length of sfx clip
        float clipLength = audioSource.clip.length;

        //destroy clip after done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
