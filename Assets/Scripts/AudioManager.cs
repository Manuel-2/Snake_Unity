using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] float minApplePitch, maxApplePitch;
    [SerializeField] AudioClip clickSound;
    [SerializeField] float minClickPitch, maxClickPitch;

    private void Awake()
    {

        if (sharedInstance == null)
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
        Invoke("RestoreDefaultPitch", duration);
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
    public void PlayClickSound()
    {
        float pitch = Random.Range(minClickPitch, maxClickPitch);
        float duration = clickSound.length;

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clickSound);
        Invoke("RestoreDefaultPitch",duration);
    }

    private void RestoreDefaultPitch()
    {
        audioSource.pitch = 1;
    }
}
