using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSounds : MonoBehaviour
{
    public float minInterval = 10.0f, maxInterval = 20.0f;
    public AudioClip[] clips;

    public float minPitch = 1.0f;
    public float maxPitch = 1.0f;
    public bool ciclcle = true;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (ciclcle == false)
        {
            var clip = clips[Random.Range(0, clips.Length)];
            if (!audioSource.isPlaying)
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(clip);
            return;
        }

        if (clips.Length > 0)
            StartCoroutine(SoundPlaying());
    }

    IEnumerator SoundPlaying()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, maxInterval));

        while (true)
        {
            var clip = clips[Random.Range(0, clips.Length)];
            if (!audioSource.isPlaying)
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                audioSource.PlayOneShot(clip);

            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval) + clip.length);
        }
    }
}
