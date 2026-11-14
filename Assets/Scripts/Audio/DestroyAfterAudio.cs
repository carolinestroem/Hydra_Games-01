using System.Collections;
using UnityEngine;

public class DestroyAfterAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        audioSource.Play();
        StartCoroutine(DestroyWhenAudioEnds());
    }

    IEnumerator DestroyWhenAudioEnds()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
