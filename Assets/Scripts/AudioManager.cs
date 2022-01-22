using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] float minApplePitch, maxApplePitch;

    private void Awake()
    {
        
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayAppleSound(AudioClip audio)
    {
        float pitch = Random.Range(minApplePitch, maxApplePitch);
        float duration = audio.length;

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audio);
        StartCoroutine(RestoreDefaultPitch(duration));
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    IEnumerator RestoreDefaultPitch(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.pitch = 1;
    }
}
